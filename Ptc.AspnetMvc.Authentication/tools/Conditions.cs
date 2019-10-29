using LinqKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace Ptc.Seteng
{

    public class Conditions<T> where T : class
    {
        private Orders _orders;
        private Filters<T> _filters;
        private Includes<T> _includes;
        private List<String> _allowProps;

        public Conditions()
        {
            this._orders = new Orders();
            this._filters = new Filters<T>();
            this._includes = new Includes<T>();
            this._allowProps = new List<String>();
        }

        public Conditions(int pageSize = 0, int pageIndex = 0) : this()
        {
            this.PageSize = pageSize;
            this.PageIndex = pageIndex;

        }
        /// <summary>
        /// 分頁(大小)
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// 分頁(頁次)
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        /// 回傳筆數
        /// </summary>
        public int TotalCount { get; set; }

        /// <summary>
        /// 設定關聯運算式
        /// </summary>
        /// <param name="key"></param>
        /// <param name="expression"></param>
        public void Include(Expression<Func<T, object>> expression)
            => _includes.Add(expression);

        /// <summary>
        /// 設定運算式
        /// </summary>
        /// <param name="prop"></param>
        /// <param name="value"></param>
        /// <param name="nodeType"></param>
        public void ConvertToFilter(string prop,
                                    ExpressionType nodeType,
                                    AppendPredicateWith predicateType,
                                    object value)
        {

            if (value == null) { return; }

            var option = CombinationExpression(prop, nodeType, value);

            _filters.Add(predicateType, option);

        }

        /// <summary>
        /// 設定運算式
        /// </summary>
        /// <param name="data"></param>
        public void ConvertToMultiFilter(List<Expression<Func<T, Boolean>>> data)
        {

            Expression<Func<T, Boolean>> exp = null;


            data.ForEach(x =>
            {
                exp = (exp == null) ? x : combine(x, exp);
            });

            if (exp != null)
                _filters.Add(AppendPredicateWith.Or, exp);

        }




        /// <summary>
        /// 設定篩選運算式(AND)
        /// </summary>
        /// <param name="key"></param>
        /// <param name="expression"></param>
        public void And(Expression<Func<T, bool>> expression)
            => _filters.Add(AppendPredicateWith.And, expression);

        /// <summary>
        /// 設定篩選運算式(OR)
        /// </summary>
        /// <param name="key"></param>
        /// <param name="expression"></param>
        public void Or(Expression<Func<T, bool>> expression)
            => _filters.Add(AppendPredicateWith.Or, expression);

        /// <summary>
        /// 新增排序運算式
        /// </summary>
        /// <param name="type"></param>
        /// <param name="expression"></param>
        public void Order(OrderType type,
                         Expression<Func<T, object>> expression)
            => this._orders.Add(type, GetExpBodyName(expression));

        /// <summary>
        /// 新增排序運算式
        /// </summary>
        /// <param name="OrderProperty"></param>
        /// <param name="type"></param>
        public void Order(OrderType type,
                          string PropName)
            => this._orders.Add(type, PropName);

  
        /// <summary>
        /// 要更新的欄位名稱
        /// </summary>
        /// <param name="expressions"></param>
        public void Allow(params Expression<Func<T, object>>[] expressions)      
           => expressions.ToList().ForEach(x => this._allowProps.Add(GetExpBodyName(x)));

        


        /// <summary>
        /// 取消更新的欄位名稱
        /// </summary>
        /// <param name="expression"></param>
        public void Allow(string PropName)
             => this._allowProps.Add(PropName);


        /// <summary>
        /// 取得關聯運算式
        /// </summary>
        public List<Expression<Func<T, object>>> Includes
            => this._includes.Get();


        /// <summary>
        /// 取得篩選運算式
        /// </summary>
        /// <returns></returns>
        public List<KeyValuePair<AppendPredicateWith, Expression<Func<T, bool>>>> Filters
            => this._filters.Get();

        /// <summary>
        /// 取得取消更新欄位的清單
        /// </summary>
        public List<String> AllowProps
            => this._allowProps;

        /// <summary>
        /// 取得排序運算式
        /// </summary>
        /// <returns></returns>
        public KeyValuePair<string, OrderType> MajorOrders
            => this._orders.GetMajor();

        public List<KeyValuePair<string, OrderType>> MinorOrders
            => this._orders.GetMinor();

        /// <summary>
        /// 清空關聯運算式
        /// </summary>
        public void ClearIncludes()
            => this._includes.Clear();

        /// <summary>
        /// 清空排序運算式
        /// </summary>
        public void ClearOrders()
            => this._orders.Clear();

        /// <summary>
        /// 清空篩選運算式
        /// </summary>
        public void ClearFilters()
            => this._filters.Clear();

        /// <summary>
        /// 清空取消更新欄位的名稱清單
        /// </summary>
        public void ClearAllow()
            => this._allowProps.Clear();

        /// <summary>
        /// combine linqkit predicate
        /// </summary>
        /// <returns></returns>
        public ExpressionStarter<T> GetPredicate()
        {
            ExpressionStarter<T> linqkitFilter = PredicateBuilder.New<T>(true);


            _filters.Get()?
                    .ToList()
                    .ForEach(x =>
                    {
                        switch (x.Key)
                        {
                            case AppendPredicateWith.And:
                                linqkitFilter = linqkitFilter.And(x.Value);
                                break;
                            case AppendPredicateWith.Or:
                                linqkitFilter = linqkitFilter.Or(x.Value);
                                break;
                        }
                    });

            return linqkitFilter;
        }

        /// <summary>
        /// 取得欄位名稱
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        private string GetExpBodyName(Expression<Func<T, object>> expression)
        {
            StringBuilder pathBuilder = new StringBuilder();

            MemberExpression pro = expression.Body as MemberExpression ??
                                  ((UnaryExpression)expression.Body).Operand as MemberExpression;



            while (pro != null)
            {

                pathBuilder.Insert(0, "." + pro.Member.Name);
                pro = pro.Expression as MemberExpression;

                return (pathBuilder.ToString(1, pathBuilder.Length - 1));
            }

            throw new NullReferenceException(" no find expression body . ");

        }

        /// <summary>
        /// 轉換數值的型別,(以防組成BinaryExpression時出錯)
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        private ConstantExpression ParseType(object val, Type orgType)
        {

            var value = GetValue(val);

            if (orgType == typeof(DateTime?))
                return Expression.Constant(value, typeof(DateTime?));

            return Expression.Constant(value);
        }

        private dynamic GetValue(object val)
        {
            if (val is Enum) return (byte)((int)val);

            return val;
        }

        /// <summary>
        /// 組合Expression
        /// </summary>
        /// <param name="prop"></param>
        /// <param name="nodeType"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public Expression<Func<T, Boolean>> CombinationExpression(string prop,
                                                                   ExpressionType nodeType,
                                                                   object value)
        {

            //宣告變數
            ParameterExpression parameter = Expression.Parameter(typeof(T), "x");
            //存取欄位
            MemberExpression propertyExp = Expression.PropertyOrField(parameter, prop);
            //存取value
            ConstantExpression propertyVal = ParseType(value, propertyExp.Type);

            //運算式
            Expression expression = null;

            //如果NodeType為parameter,就走contains語法
            if (nodeType == ExpressionType.Parameter)
            {
                MethodInfo method = typeof(string).GetMethod("Contains", new[] { typeof(string) });
                expression = Expression.Call(propertyExp, method, propertyVal);
            }
            else
            {
                expression = Expression.MakeBinary(
                nodeType,
                propertyExp,
                propertyVal);
            }
            return Expression.Lambda<Func<T, bool>>(expression, parameter);

        }

        /// <summary>
        /// 將運算式合併
        /// </summary>
        /// <param name="expressionOne"></param>
        /// <param name="expressionTwo"></param>
        /// <returns></returns>
        public Expression<Func<T, Boolean>> combine(Expression<Func<T, Boolean>> expressionOne,
                                                    Expression<Func<T, Boolean>> expressionTwo)
        {

            var invokedSecond = Expression.Invoke(expressionTwo, expressionOne.Parameters.Cast<Expression>());


            return Expression.Lambda<Func<T, Boolean>>(
             Expression.And(expressionOne.Body, invokedSecond), expressionOne.Parameters
            );
        }

    }


}



