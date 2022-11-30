using Bat.Core;

namespace Bat.EntityFrameworkCore;

public static class DbContextExtention
{
    public async static Task<List<TResult>> ExecuteProcedure<TResult>(this DbContext dbContext, string sqlQuery, params object[] parameters) where TResult : class
    {
        using var procedureContext = new BatProcedureDbContext<TResult>(dbContext.Database.GetConnectionString());
        return await procedureContext.ResultSet.FromSqlRaw(sqlQuery, parameters).ToListAsync();
    }

    public async static Task<bool> ExecuteCommandAsync<TEntity>(this DbContext dbContext, string sql, params object[] parameters) where TEntity : class
       => await dbContext.Database.ExecuteSqlRawAsync(sql, parameters) >= 0;

    public static IQueryable<TEntity> ExecuteQuery<TEntity>(this DbContext dbContext, string sql, params object[] parameters) where TEntity : class
        => dbContext.Set<TEntity>().FromSqlRaw(sql, parameters);

    public async static Task<List<TEntity>> ExecuteQueryListAsync<TEntity>(this DbContext dbContext, string sql, params object[] parameters) where TEntity : class
        => await dbContext.Set<TEntity>().FromSqlRaw(sql, parameters).ToListAsync();


    public static bool ContainsEntity<TEntity>(this DbContext dbContext) where TEntity : class
        => dbContext.Model.FindEntityType(typeof(TEntity)) != null;

    public static IEnumerable<EntityEntry> GetAddOrUpdateEntity(this DbContext dbContext)
        => dbContext.ChangeTracker.Entries().Where(x => x.State == EntityState.Added || x.State == EntityState.Modified);

    public static IEnumerable<EntityEntry> GetAddedEntity(this DbContext dbContext)
        => dbContext.GetChangedEntity(EntityState.Deleted);

    public static IEnumerable<EntityEntry> GetUpdatedEntity(this DbContext dbContext)
        => dbContext.GetChangedEntity(EntityState.Deleted);

    public static IEnumerable<EntityEntry> GetDeletedEntity(this DbContext dbContext)
        => dbContext.GetChangedEntity(EntityState.Deleted);

    public static IEnumerable<EntityEntry> GetChangedEntity(this DbContext dbContext, EntityState? entityState = null)
    {
        var entries = dbContext.ChangeTracker.Entries();
        if (entityState.HasValue) entries = entries.Where(x => x.State == entityState.Value);
        return entries;
    }

    public static void BasePropertiesInitializer(this DbContext dbContext)
    {
        foreach (var entry in dbContext.ChangeTracker.Entries<IBaseProperties>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    {
                        if (entry.Entity is IInsertDateProperty insertDateProperty)
                        {
                            insertDateProperty.InsertDateMi = DateTime.Now;
                        }
                        if (entry.Entity is IInsertDateProperties insertDateProperties)
                        {
                            insertDateProperties.InsertDateMi = DateTime.Now;
                            insertDateProperties.InsertDateSh = PersianDateTime.Now.ToString(PersianDateTimeFormat.Date);
                        }

                        if (entry.Entity is IModifyDateProperty modifyDateProperty)
                        {
                            modifyDateProperty.ModifyDateMi = DateTime.Now;
                        }
                        if (entry.Entity is IModifyDateProperties modifyDateProperties)
                        {
                            modifyDateProperties.ModifyDateMi = DateTime.Now;
                            modifyDateProperties.ModifyDateSh = PersianDateTime.Now.ToString(PersianDateTimeFormat.Date);
                        }
                        break;
                    }
                case EntityState.Modified:
                    {
                        if (entry.Entity is IModifyDateProperty modifyDateProperty)
                        {
                            modifyDateProperty.ModifyDateMi = DateTime.Now;
                        }
                        if (entry.Entity is IModifyDateProperties modifyDateProperties)
                        {
                            modifyDateProperties.ModifyDateMi = DateTime.Now;
                            modifyDateProperties.ModifyDateSh = PersianDateTime.Now.ToString(PersianDateTimeFormat.Date);
                        }
                        break;
                    }
                case EntityState.Deleted:
                    {
                        if (entry.Entity is ISoftDeleteProperty softDeleteProperty)
                        {
                            softDeleteProperty.IsDeleted = true;
                            entry.State = EntityState.Modified;
                        }
                        break;
                    }
            }
        }
    }

    public static Dictionary<string, ValidationError> ValidateContext(this DbContext dbContext)
    {
        var result = new Dictionary<string, ValidationError>();
        foreach (var entity in dbContext.GetAddOrUpdateEntity().Select(x => x.Entity))
        {
            try
            {
                var validationContext = new ValidationContext(entity);
                Validator.ValidateObject(
                    entity,
                    validationContext,
                    validateAllProperties: true);
            }
            catch (ValidationException validationException)
            {
                result.Add(entity.GetType().FullName, new ValidationError
                {
                    Value = validationException.Value.ToString(),
                    ValidationSource = validationException.Source,
                    Field = validationException.ValidationResult.MemberNames.First(),
                    ValidationMessage = validationException.ValidationResult.ErrorMessage
                });
            }
        }
        return result;
    }

    public static void SaveAuditLog<TEntity>(this DbContext dbContext, string userId) where TEntity : class, new()
    {
        try
        {
            if (!dbContext.ContainsEntity<TEntity>()) return;

            var auditList = new List<TEntity>();
            var tableName = string.Empty;
            foreach (var entry in dbContext.GetChangedEntity())
            {
                tableName = entry.Entity.GetType().FullName;
                tableName = tableName.Contains('_') ? tableName[..tableName.IndexOf('_')] : tableName;
                var audit = new TEntity() as IAuditLogProperties;
                switch (entry.State)
                {
                    case EntityState.Added:
                        {
                            #region Added
                            audit.UserId = userId;
                            audit.ActionType = EntityState.Added.ToString();
                            audit.EntityName = tableName;
                            audit.NewValue = entry.CurrentValues.ToObject().SerializeDbSetToJson();
                            audit.OldValue = null;
                            audit.InsertDateMi = DateTime.Now;
                            audit.InsertDateSh = PersianDateTime.Now.ToString(PersianDateTimeFormat.Date);
                            break;
                            #endregion
                        }
                    case EntityState.Modified:
                        {
                            #region Modified
                            audit.UserId = userId;
                            audit.ActionType = EntityState.Modified.ToString();
                            audit.EntityName = tableName;
                            audit.NewValue = entry.CurrentValues.ToObject().SerializeDbSetToJson();
                            audit.OldValue = entry.GetDatabaseValues().ToObject().SerializeDbSetToJson();
                            audit.InsertDateMi = DateTime.Now;
                            audit.InsertDateSh = PersianDateTime.Now.ToString(PersianDateTimeFormat.Date);
                            break;
                            #endregion
                        }
                    case EntityState.Deleted:
                        {
                            #region Deleted
                            audit.UserId = userId;
                            audit.ActionType = EntityState.Deleted.ToString();
                            audit.EntityName = tableName;
                            audit.NewValue = null;
                            audit.OldValue = entry.OriginalValues.ToObject().SerializeDbSetToJson();
                            audit.InsertDateMi = DateTime.Now;
                            audit.InsertDateSh = PersianDateTime.Now.ToString(PersianDateTimeFormat.Date);
                            break;
                            #endregion
                        }
                }
                auditList.Add(audit as TEntity);
            }
            dbContext.Set<TEntity>().AddRange(auditList);
        }
        catch { }
    }



    public static string GetConnectionString(this DbContext dbContext)
        => dbContext.Database.GetConnectionString();

    public static SqlConnection GetSqlConnection(this DbContext dbContext)
        => new(dbContext.Database.GetConnectionString());
}