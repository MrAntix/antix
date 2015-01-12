using System.Collections.Generic;
using System.Linq.Expressions;

namespace Antix.Services.Validation
{
    public class ExpressionPathVisitor :
        ExpressionVisitor
    {
        public static string GetPath(Expression expression)
        {
            var visitor = new ExpressionPathVisitor();
            visitor.Visit(expression);

            return string.Join(".", visitor._path);
        }

        readonly List<string> _path = new List<string>();

        protected override Expression VisitMember(MemberExpression node)
        {
            _path.Add(node.Member.Name);

            return base.VisitMember(node);
        }
    }
}