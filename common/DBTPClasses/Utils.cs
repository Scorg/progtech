using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Infrastructure;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Windows.Forms;

namespace common {
	public static class Utils {
		public static List<string> GetEntityPKNames<TContext, TEntity>(TContext c)
			where TContext : DbContext
			where TEntity : class
		{
			var objctx = ((IObjectContextAdapter)c).ObjectContext;
			
			List<string> keys = objctx.CreateObjectSet<TEntity>().EntitySet.ElementType.KeyMembers.Select(x => x.Name).ToList();
			return keys;
		}

		public static object[] GetEntityPK<TContext, TEntity>(TContext c, TEntity e)
			where TContext : DbContext
			where TEntity : class
		{
			return GetEntityPKNames<TContext, TEntity>(c).Select(x => e.GetType().GetProperty(x).GetValue(e)).ToArray();
		}

		//public static Expression<Func<S1, R2>> Insert<S1, R1, R2>(Expression<Func<S1, R1>> inner, Expression<Func<R1, R2>> outer)
		//{
		//	return /*ParameterRebinder.ReplaceParameters(dic, */Expression.Call((outer.Body).Method, , inner)/*)*/;
		//}

		public static DbContext GetDbContextFromEntity(object entity)
		{
			ObjectContext object_context = GetObjectContextFromEntity(entity);

			if (object_context == null)
				return null;

			return new DbContext(object_context, false);
		}

		private static ObjectContext GetObjectContextFromEntity(object entity)
		{
			var field = entity.GetType().GetField("_entityWrapper");

			if (field == null)
				return null;

			var wrapper  = field.GetValue(entity);
			var property = wrapper.GetType().GetProperty("Context");
			var context  = (ObjectContext)property.GetValue(wrapper, null);

			return context;
		}

		public static bool All<T>(this List<T> list, Func<T, int, bool> pred)
		{
			for (int i=0; i<list.Count; ++i) if (!pred(list[i], i)) return false;
			return true;
		}
	}

	public class ParameterRebinder : ExpressionVisitor {
		private readonly Dictionary<ParameterExpression, ParameterExpression> map;

		public ParameterRebinder(Dictionary<ParameterExpression, ParameterExpression> map)
		{
			this.map = map ?? new Dictionary<ParameterExpression, ParameterExpression>();
		}

		public static Expression ReplaceParameters(Dictionary<ParameterExpression, ParameterExpression> map, Expression exp)
		{
			return new ParameterRebinder(map).Visit(exp);
		}

		protected override Expression VisitParameter(ParameterExpression p)
		{
			ParameterExpression replacement;

			if (map.TryGetValue(p, out replacement)) {
				p = replacement;
			}

			return base.VisitParameter(p);
		}
	}
}
