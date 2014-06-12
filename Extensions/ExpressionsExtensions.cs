using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Toolkit.Extensions
{
    public static class ExpressionsExtensions
    {
        public static string GetParameterName(this Expression<Func<object>> parameterAccesor)
        {
            if (parameterAccesor.NodeType != ExpressionType.Lambda)
            {
                throw new NotSupportedException("You must pass in a lambda expression accessing a property");
            }
            Expression body = parameterAccesor.Body;
            MemberExpression memberExpression = null;
            if (body.NodeType == ExpressionType.Convert)
            {
                UnaryExpression convertExpression = (UnaryExpression)body;
                memberExpression = (MemberExpression)convertExpression.Operand;
            } 
            else if (body.NodeType == ExpressionType.MemberAccess) {
                memberExpression = (MemberExpression)body;
            }
            else
            {
                throw new NotSupportedException(String.Format("The lambda expression body is of type {0} and that is unspported", body.NodeType));
            }
            string propertyName = memberExpression.Member.Name;
            return propertyName;
        }
    }
}
