using System;
using System.Linq.Expressions;

namespace Bat.Core
{
    public class PagingQueryFilterWithSelector<TEntity, TResult> : PagingQueryFilter<TEntity> where TEntity : class
    {
        public Expression<Func<TEntity, TResult>> Selector { set; get; }
    }
}