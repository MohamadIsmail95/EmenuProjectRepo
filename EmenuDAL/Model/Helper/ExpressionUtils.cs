using EmenuDAL.Model.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace EmenuDAL.Model.Helper
{
    public static partial class ExpressionUtils
    {
        public static Expression<Func<T, bool>> BuildPredicate<T>(List<FilterObjectList> filter)
        {
            try
            {
                var parameter = Expression.Parameter(typeof(T), "x");
                Expression body2 = null;
                Expression body1 = null;
                Expression bodytemp1 = null;
                Expression bodytemp2 = null;
                Expression binExp = null;
                Expression body = parameter;

                foreach (var item in filter)
                {
                    bodytemp1 = null;
                    bodytemp2 = null;
                    binExp = null;
                    body = parameter;
                    foreach (var subMember in item.key.Split('.'))
                    {
                        body = Expression.PropertyOrField(body, subMember);

                    }

                    foreach (var item2 in item.value)
                    {
                        object val;
                        try
                        {
                            var s = Convert.ChangeType(item2, body.Type);
                            val = Convert.ChangeType(item2, body.Type);


                        }
                        catch (Exception)
                        {
                            val = Int32.Parse(item2.ToString());
                        }

                        Expression bodytemp;
                        try
                        {
                            if (val == "List")
                            {
                                bodytemp = Expression.NotEqual(body, Expression.Constant(val, body.Type));

                            }

                            else if (item.key == "key")

                            {
                                ConstantExpression constant = Expression.Constant(val, body.Type);
                                Expression left = Expression.Call(body, typeof(string).GetMethod("ToLower", System.Type.EmptyTypes));
                                Expression right = Expression.Call(constant, typeof(string).GetMethod("ToLower", System.Type.EmptyTypes));
                                bodytemp = Expression.Call(left, "StartsWith", null, right);

                            }


                            else
                            {
                                bodytemp = Expression.Equal(body, Expression.Constant(val, body.Type));

                            }



                        }
                        catch (Exception e)
                        {
                            val = float.Parse(item2.ToString());
                            bodytemp = Expression.Equal(body, Expression.Constant(val, body.Type));
                        }
                        //bodytemp = Expression.Equal(body, Expression.Constant(val, body.Type));
                        if (bodytemp1 == null)
                        {
                            bodytemp1 = bodytemp;
                        }
                        else
                        {
                            bodytemp2 = bodytemp;
                            binExp = Expression.Or(bodytemp1, bodytemp2);
                            bodytemp1 = binExp;
                        }
                    }

                    if (body1 == null)
                    {
                        body1 = bodytemp1;
                        binExp = body1;
                    }
                    else
                    {
                        body2 = bodytemp1;
                        binExp = Expression.And(body1, body2);
                        body1 = binExp;
                    }
                }

                var x = Expression.Lambda<Func<T, bool>>(binExp, parameter);
                return Expression.Lambda<Func<T, bool>>(binExp, parameter);
            }
            catch (Exception e)
            {
                return null;
            }




        }

        
    }
}
