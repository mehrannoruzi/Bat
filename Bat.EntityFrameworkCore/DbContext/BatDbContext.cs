namespace Bat.EntityFrameworkCore;

public abstract class BatDbContext : DbContext, IBatDbContext
{
    protected BatDbContext() { }

    protected BatDbContext(DbContextOptions options) : base(options) { }

    protected BatDbContext(DbContextOptions<BatDbContext> options) : base(options) { }


    public virtual void ApplyPersianYK()
    {
        var changedEntitis = this.GetChangedEntity();
        foreach (var item in changedEntitis)
        {
            if (item.Entity == null) continue;

            var propertyInfo = item.Entity.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(x => x.CanRead && x.CanWrite && x.PropertyType == typeof(string));
            foreach (var property in propertyInfo)
            {
                var value = property.GetValue(item.Entity);
                if (value != null)
                {
                    var newValue = value.ToString().ToPersianCharacters();
                    if (newValue != value.ToString()) property.SetValue(item.Entity, newValue);
                }
            }
        }
    }

    public virtual void ApplyEnglishNumber()
    {
        var changedEntitis = this.GetChangedEntity();
        foreach (var item in changedEntitis)
        {
            if (item.Entity == null) continue;

            var propertyInfo = item.Entity.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(x => x.CanRead && x.CanWrite &&
                (x.PropertyType == typeof(string) ||
                x.PropertyType == typeof(int) || x.PropertyType == typeof(long) ||
                x.PropertyType == typeof(float) || x.PropertyType == typeof(double)));
            foreach (var property in propertyInfo)
            {
                var value = property.GetValue(item.Entity);
                if (value != null)
                {
                    var newValue = value.ToString().ToEnglishNumber();
                    if (newValue != value.ToString()) property.SetValue(item.Entity, newValue);
                }
            }
        }
    }


    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        ApplyPersianYK();
        ApplyEnglishNumber();

        this.BasePropertiesInitializer();

        return await base.SaveChangesAsync(cancellationToken);
    }

    public virtual async Task<SaveChangeResult> BatSaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var result = new SaveChangeResult();
        try
        {
            ApplyPersianYK();
            ApplyEnglishNumber();

            this.BasePropertiesInitializer();

            result.Result = await base.SaveChangesAsync(cancellationToken);
            result.IsSuccessful = result.Result.ToSaveChangeResult();
            result.Message = result.Result.ToSaveChangeResultMessage(Strings.Success, Strings.UnknownException);
            result.ResultType = result.IsSuccessful ? SaveChangeResultType.Success : SaveChangeResultType.UnknownException;

            return result;
        }
        catch (ValidationException validationException)
        {
            #region Validation Exception
            result.IsSuccessful = false;
            result.Exception = validationException;
            result.ValidationErrors = this.ValidateContext();
            result.Message = Strings.EntityValidationException;
            result.ResultType = SaveChangeResultType.EntityValidationException;
            return result;
            #endregion
        }
        catch (DbUpdateConcurrencyException concurrencyException)
        {
            #region Concurrency Exception
            result.IsSuccessful = false;
            result.Exception = concurrencyException;
            result.Message = Strings.UpdateConcurrencyException;
            result.ResultType = SaveChangeResultType.UpdateConcurrencyException;
            return result;
            #endregion
        }
        catch (DbUpdateException updateException)
        {
            #region Update Exception
            if ((updateException.InnerException.IsNotNull() && updateException.InnerException.Message.ToLower().Contains("cannot insert duplicate key")) ||
                        (updateException.InnerException.InnerException.IsNotNull() && updateException.InnerException.InnerException.Message.ToLower().Contains("cannot insert duplicate key")))
            {
                result.IsSuccessful = false;
                result.Exception = updateException;
                result.Message = Strings.DuplicateIndexKeyException;
                result.ResultType = SaveChangeResultType.DuplicateIndexKeyException;
                return result;
            }

            result.IsSuccessful = false;
            result.Exception = updateException;
            result.Message = Strings.UpdateException;
            result.ResultType = SaveChangeResultType.UpdateException;
            return result;
            #endregion
        }
        catch (Exception exception)
        {
            #region Public Exception
            if (exception.Message.ToLower().Contains("cannot insert duplicate key"))
            {
                result.IsSuccessful = false;
                result.Exception = exception;
                result.Message = Strings.DuplicateIndexKeyException;
                result.ResultType = SaveChangeResultType.DuplicateIndexKeyException;
                return result;
            };

            result.IsSuccessful = false;
            result.Exception = exception;
            result.Message = Strings.UnknownException;
            result.ResultType = SaveChangeResultType.UnknownException;
            return result;
            #endregion
        }
    }

    public virtual async Task<SaveChangeResult> BatSaveChangesWithValidationAsync(CancellationToken cancellationToken = default)
    {
        var result = new SaveChangeResult();
        try
        {
            var validationError = this.ValidateContext();
            if (validationError.Count > 0)
            {
                #region Validation Exception
                result.IsSuccessful = false;
                result.ValidationErrors = validationError;
                result.Exception = new ValidationException();
                result.ResultType = SaveChangeResultType.EntityValidationException;
                return result;
                #endregion
            }

            ApplyPersianYK();
            ApplyEnglishNumber();

            this.BasePropertiesInitializer();

            result.Result = await base.SaveChangesAsync(cancellationToken);
            result.IsSuccessful = result.Result.ToSaveChangeResult();
            result.Message = result.Result.ToSaveChangeResultMessage(Strings.Success, Strings.UnknownException);
            result.ResultType = result.IsSuccessful ? SaveChangeResultType.Success : SaveChangeResultType.UnknownException;

            return result;
        }
        catch (DbUpdateConcurrencyException concurrencyException)
        {
            #region Concurrency Exception
            result.IsSuccessful = false;
            result.Exception = concurrencyException;
            result.Message = Strings.UpdateConcurrencyException;
            result.ResultType = SaveChangeResultType.UpdateConcurrencyException;
            return result;
            #endregion
        }
        catch (DbUpdateException updateException)
        {
            #region Update Exception
            if ((updateException.InnerException.IsNotNull() && updateException.InnerException.Message.ToLower().Contains("cannot insert duplicate key")) ||
                        (updateException.InnerException.InnerException.IsNotNull() && updateException.InnerException.InnerException.Message.ToLower().Contains("cannot insert duplicate key")))
            {
                result.IsSuccessful = false;
                result.Exception = updateException;
                result.Message = Strings.DuplicateIndexKeyException;
                result.ResultType = SaveChangeResultType.DuplicateIndexKeyException;
                return result;
            }

            result.IsSuccessful = false;
            result.Exception = updateException;
            result.Message = Strings.UpdateException;
            result.ResultType = SaveChangeResultType.UpdateException;
            return result;
            #endregion
        }
        catch (Exception exception)
        {
            #region Public Exception
            if (exception.Message.ToLower().Contains("cannot insert duplicate key"))
            {
                result.IsSuccessful = false;
                result.Exception = exception;
                result.Message = Strings.DuplicateIndexKeyException;
                result.ResultType = SaveChangeResultType.DuplicateIndexKeyException;
                return result;
            };

            result.IsSuccessful = false;
            result.Exception = exception;
            result.Message = Strings.UnknownException;
            result.ResultType = SaveChangeResultType.UnknownException;
            return result;
            #endregion
        }
    }
}