using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specifications
{
    public class BaseSpecification<T> : ISpecification<T>
    {
        public BaseSpecification()
        {
        }

        public BaseSpecification(Expression<Func<T, bool>> criteria)
        {
            Criteria = criteria;
        }

        public Expression<Func<T, bool>> Criteria { get; }

        public List<string> IncludeStrings { get; } = new List<string>();

        protected void AddInclude(Expression<Func<T, object>> includeExpression)
        {
            //expression product => product.Category.TipoFilho vira Category.TipoFilho
            string expressionString = String.Join(".", includeExpression.Body.ToString().Split('.').Skip(1));
            IncludeStrings.Add(expressionString);
        }
    }
}
