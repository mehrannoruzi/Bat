using Bat.Core;
using System.Linq;

namespace Bat.EntityFrameworkCore.Tools
{
    public static class PublicExtention
    {
        public static bool ToSaveChangeResult(this int value)
        {
            if (value >= 0) return true;

            return false;
        }

        public static string ToSaveChangeMessageResult(this int value, string successMessage, string errorMessage)
        {
            if (value < 0) return errorMessage;

            return successMessage;
        }

        public static string ToSaveChangeMessageResult(this bool value, string successMessage, string errorMessage)
        {
            if (value) return successMessage;

            return errorMessage;
        }

        public static void ChangeIsActiveStatus<T>(this T entity) where T : IIsActiveProperty
        {
            ((IIsActiveProperty)entity).IsActive = !((IIsActiveProperty)entity).IsActive;
        }

        public static IQueryable<T> IsActiveFilter<T>(this IQueryable<T> collection, bool? isActive = true) where T : class, IIsActiveProperty
        {
            if (isActive == null) return collection;

            collection = collection.Where(x => x.IsActive == isActive);
            return collection;
        }

        public static void ChangeIsDeletedStatus<T>(this T entity) where T : ISoftDeleteProperty
        {
            ((ISoftDeleteProperty)entity).IsDeleted = !((ISoftDeleteProperty)entity).IsDeleted;
        }
    }
}
