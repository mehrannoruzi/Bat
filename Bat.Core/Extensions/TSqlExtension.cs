using System;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Collections;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Bat.Core
{
    public class WherePart
    {
        public string TSql { get; set; }

        public Dictionary<string, object> Parameters { get; set; } = new Dictionary<string, object>();

        public static WherePart HaveSql(string sql)
        {
            return new WherePart()
            {
                Parameters = new Dictionary<string, object>(),
                TSql = sql
            };
        }

        public static WherePart HaveParameter(int count, object value)
        {
            return new WherePart()
            {
                Parameters = { { count.ToString(), value } },
                TSql = $"@{count}"
            };
        }

        public static WherePart HaveCollectionParameter(ref int countStart, IEnumerable values)
        {
            var parameters = new Dictionary<string, object>();
            var sql = new StringBuilder("(");
            foreach (var value in values)
            {
                parameters.Add((countStart).ToString(), value);
                sql.Append($"@{countStart},");
                countStart++;
            }
            if (sql.Length == 1)
            {
                sql.Append("null,");
            }
            sql[sql.Length - 1] = ')';
            return new WherePart()
            {
                Parameters = parameters,
                TSql = sql.ToString()
            };
        }

        public static WherePart Concat(string @operator, WherePart operand)
        {
            return new WherePart()
            {
                Parameters = operand.Parameters,
                TSql = $"({@operator} {operand.TSql})"
            };
        }

        public static WherePart Concat(WherePart left, string @operator, WherePart right)
        {
            return new WherePart()
            {
                Parameters = left.Parameters.Union(right.Parameters).ToDictionary(kvp => kvp.Key, kvp => kvp.Value),
                TSql = $"({left.TSql} {@operator} {right.TSql})"
            };
        }
    }

    public static class TSqlExtension
    {
        private static object GetValue(Expression member)
        {
            // source: http://stackoverflow.com/a/2616980/291955
            var objectMember = Expression.Convert(member, typeof(object));
            var getterLambda = Expression.Lambda<Func<object>>(objectMember);
            var getter = getterLambda.Compile();
            return getter();
        }

        private static string NodeTypeToString(ExpressionType nodeType)
        {
            switch (nodeType)
            {
                case ExpressionType.Add:
                    return "+";
                case ExpressionType.And:
                    return "&";
                case ExpressionType.AndAlso:
                    return "AND";
                case ExpressionType.Divide:
                    return "/";
                case ExpressionType.Equal:
                    return "=";
                case ExpressionType.ExclusiveOr:
                    return "^";
                case ExpressionType.GreaterThan:
                    return ">";
                case ExpressionType.GreaterThanOrEqual:
                    return ">=";
                case ExpressionType.LessThan:
                    return "<";
                case ExpressionType.LessThanOrEqual:
                    return "<=";
                case ExpressionType.Modulo:
                    return "%";
                case ExpressionType.Multiply:
                    return "*";
                case ExpressionType.Negate:
                    return "-";
                case ExpressionType.Not:
                    return "NOT";
                case ExpressionType.NotEqual:
                    return "<>";
                case ExpressionType.Or:
                    return "|";
                case ExpressionType.OrElse:
                    return "OR";
                case ExpressionType.Subtract:
                    return "-";
            }
            throw new Exception($"Unsupported node type: {nodeType}");
        }

        private static WherePart Recurse(ref int i, Expression expression, bool isUnary = false, string prefix = null, string postfix = null)
        {
            if (expression is UnaryExpression)
            {
                var unary = (UnaryExpression)expression;
                return WherePart.Concat(NodeTypeToString(unary.NodeType), Recurse(ref i, unary.Operand, true));
            }
            if (expression is BinaryExpression)
            {
                var body = (BinaryExpression)expression;
                return WherePart.Concat(Recurse(ref i, body.Left), NodeTypeToString(body.NodeType), Recurse(ref i, body.Right));
            }
            if (expression is ConstantExpression)
            {
                var constant = (ConstantExpression)expression;
                var value = constant.Value;
                if (value is int)
                {
                    return WherePart.HaveSql(value.ToString());
                }
                if (value is string)
                {
                    if (prefix == null && postfix == null)
                        value = "'" + (string)value + "'";
                    else
                        value = prefix + (string)value + postfix;
                }
                if (value is DateTime)
                {
                    value = "'" + value + "'";
                }
                if (value is DateTime?)
                {
                    value = "'" + (string)value + "'";
                }
                if (value is TimeSpan)
                {
                    value = "'" + value + "'";
                }
                if (value is TimeSpan?)
                {
                    value = "'" + (string)value + "'";
                }
                if (value is bool && isUnary)
                {
                    return WherePart.Concat(WherePart.HaveParameter(i++, value), "=", WherePart.HaveSql("1"));
                }
                return WherePart.HaveParameter(i++, value);
            }
            if (expression is MemberExpression)
            {
                var member = (MemberExpression)expression;
                //if (member.Member is PropertyInfo)
                //{
                //    var property = (PropertyInfo)member.Member;
                //    //var colName = _tableDef.GetColumnNameFor(property.Name);
                //    var colName = property.Name;
                //    if (isUnary && member.Type == typeof(bool))
                //    {
                //        return WherePart.Concat(Recurse(ref i, expression), "=", WherePart.HaveParameter(i++, true));
                //    }
                //    return WherePart.HaveSql("[" + colName + "]");
                //}
                if (member.Member is FieldInfo)
                {
                    var value = GetValue(member);
                    if (value is string)
                    {
                        value = prefix + (string)value + postfix;
                    }
                    return WherePart.HaveParameter(i++, value);
                }
                throw new Exception($"Expression does not refer to a property or field: {expression}");
            }
            if (expression is MethodCallExpression)
            {
                var methodCall = (MethodCallExpression)expression;
                // LIKE queries:
                if (methodCall.Method == typeof(string).GetMethod("Contains", new[] { typeof(string) }))
                {
                    return WherePart.Concat(Recurse(ref i, methodCall.Object), "LIKE", Recurse(ref i, methodCall.Arguments[0], prefix: "'%", postfix: "%'"));
                }
                if (methodCall.Method == typeof(string).GetMethod("StartsWith", new[] { typeof(string) }))
                {
                    return WherePart.Concat(Recurse(ref i, methodCall.Object), "LIKE", Recurse(ref i, methodCall.Arguments[0], prefix: "'", postfix: "%'"));
                }
                if (methodCall.Method == typeof(string).GetMethod("EndsWith", new[] { typeof(string) }))
                {
                    return WherePart.Concat(Recurse(ref i, methodCall.Object), "LIKE", Recurse(ref i, methodCall.Arguments[0], prefix: "'%", postfix: "'"));
                }
                // IN queries:
                if (methodCall.Method.Name == "Contains")
                {
                    Expression collection;
                    Expression property;
                    if (methodCall.Method.IsDefined(typeof(ExtensionAttribute)) && methodCall.Arguments.Count == 2)
                    {
                        collection = methodCall.Arguments[0];
                        property = methodCall.Arguments[1];
                    }
                    else if (!methodCall.Method.IsDefined(typeof(ExtensionAttribute)) && methodCall.Arguments.Count == 1)
                    {
                        collection = methodCall.Object;
                        property = methodCall.Arguments[0];
                    }
                    else
                    {
                        throw new Exception("Unsupported method call: " + methodCall.Method.Name);
                    }
                    var values = (IEnumerable)GetValue(collection);
                    return WherePart.Concat(Recurse(ref i, property), "IN", WherePart.HaveCollectionParameter(ref i, values));
                }
                throw new Exception("Unsupported method call: " + methodCall.Method.Name);
            }
            throw new Exception("Unsupported expression: " + expression.GetType().Name);
        }



        public static string ToSelectPart<T>(string tableName, List<string> columnsName = null, int top = 0)
        {
            string columns = string.Empty;
            if (columnsName == null || columnsName.Count() == 0) columns = "*";
            else
            {
                foreach (var column in columnsName)
                {
                    columns += column + ", ";
                }
                columns = columns.Substring(0, columns.Length - 1);
            }

            return top == 0 ? $"SELECT {columns} FROM {tableName} " : $"SELECT TOP {top} {columns} FROM {tableName} ";
        }

        public static string ToSelectPart<T>(Expression<Func<T, bool>> expression, List<string> columnsName = null, int top = 0)
        {
            if (expression == null) return string.Empty;
            string columns = string.Empty;
            if (columnsName == null || columnsName.Count() == 0) columns = "*";
            else
            {
                foreach (var column in columnsName)
                {
                    columns += column + ", ";
                }
                columns = columns.Substring(0, columns.Length - 1);
            }

            return top == 0 ? $"SELECT {columns} FROM {expression.Parameters?.First().Type.Name} " : $"SELECT TOP {top} {columns} FROM {expression.Parameters?.First().Type.Name} ";
        }

        public static WherePart ToWherePart<T>(Expression<Func<T, bool>> expression)
        {
            if (expression == null) return new WherePart();
            var i = 1;
            return Recurse(ref i, expression.Body, isUnary: true);
        }

        public static string ToRawWherePart<T>(Expression<Func<T, bool>> expression)
        {
            if (expression == null) return string.Empty;
            var i = 1;
            var whereParts = Recurse(ref i, expression.Body, isUnary: true);

            if (!whereParts.Parameters.Any()) return whereParts.TSql;

            StringBuilder finalQuery = new StringBuilder();
            finalQuery.Append(whereParts.TSql);
            foreach (var p in whereParts.Parameters)
            {
                var val = "@" + p.Key;
                finalQuery = finalQuery.Replace(val, p.Value.ToString());
            }
            return finalQuery.ToString();
        }

        public static string ToOrderByPart<T>(Expression<Func<IQueryable<T>, IOrderedQueryable<T>>> expression)
        {
            var fields = expression.Body.ToString().Split(".OrderBy");
            var orderByPart = string.Empty;
            if (fields.Count() > 1)
            {
                orderByPart += " ORDER BY ";
                foreach (var field in fields)
                {
                    if (field.StartsWith("("))
                    {
                        orderByPart += $"{field.Substring(field.IndexOf('.') + 1, field.IndexOf(')') - field.IndexOf('.') - 1)} ASC, ";
                    }
                    else if (field.StartsWith("Des"))
                    {
                        orderByPart += $"{field.Substring(field.IndexOf('.') + 1, field.IndexOf(')') - field.IndexOf('.') - 1)} DESC, ";
                    }
                }
                orderByPart = orderByPart.Substring(0, orderByPart.Length - 2);
            }

            return orderByPart;
        }



        public static string ToTSql(string selectPart, WherePart wherePart = null, string orderBy = null, int skipRow = 0)
            => selectPart + "WHERE " + wherePart?.TSql + orderBy + (skipRow != 0 ? $" OFFSET {skipRow} ROWS" : string.Empty);

        public static string ToTSql(string selectPart, string wherePart = null, string orderBy = null, int skipRow = 0)
            => selectPart + "WHERE " + wherePart + orderBy + (skipRow != 0 ? $" OFFSET {skipRow} ROWS" : string.Empty);
    }
}