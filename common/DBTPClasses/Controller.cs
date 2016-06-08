using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace common {
	public class Controller<TContext> : IController where TContext : class
	{
		protected Control mParent;
		protected object[] mEntityKey;

		protected Controller<TContext> mParentController;
		protected ControllerCollection<TContext> mChildControllers;

		public Controller(Control parent = null)
		{
			mParent = parent;
			mParentController=null;
			mChildControllers=new ControllerCollection<TContext>(this);
		}

		public virtual void Show(Control parent=null)
		{
		}

		protected virtual void OnParentFormShowing(TContext ctx) { }
		protected virtual bool OnParentFormValidating() { return true; }
		protected virtual bool OnParentFormSaving<TEntity>(TEntity entity) { return true; }
		protected virtual void OnParentFormDiscarding(/* передать что-то? */) { }

		protected virtual void ShowChildren(TContext ctx)
		{
			foreach (Controller<TContext> ctl in Controllers) {
				ctl.OnParentFormShowing(ctx);
			}
		}

		protected virtual bool ValidateChildren()
		{
			bool res = true;
			foreach (Controller<TContext> ctl in Controllers) {
				res = res && ctl.OnParentFormValidating();
			}
			return res;
		}

		protected virtual bool SaveChildren<T>(T ent)
		{
			bool res = true;
			foreach (Controller<TContext> ctl in Controllers) {
				res = res && ctl.OnParentFormSaving<T>(ent);
			}
			return res;

		}

		protected virtual void DiscardChildren()
		{
			foreach (Controller<TContext> ctl in Controllers) {
				ctl.OnParentFormDiscarding();
			}
		}

		public virtual ControllerCollection<TContext> Controllers
		{
			get { return mChildControllers; }
		}

		public virtual Controller<TContext> Parent
		{
			get { return mParentController; }
			set
			{
				//Удаляем из старого, если он существует и если мы там ещё находимся (доступ не из ControllerCollection
				if (mParentController!=null && value!=mParentController && mParentController.Controllers.Contains(this)) {
					mParentController.Controllers.Remove(this);
				}

				mParentController = value;

				//Присваиваем новому, если нас там ещё нет
				if (mParentController!=null && !mParentController.Controllers.Contains(this)) {
					mParentController.Controllers.Add(this);
				}
			}
		}

		public virtual ICollection<TEntity> GetCollection<TEntity>(string property, TContext ctx=null) where TEntity : class
		{
			return null;
		}

		public virtual IQueryable<TEntity> GetCollectionQuery<TEntity>(string property, TContext ctx=null) where TEntity : class
		{
			return null;
		}

		/*internal void _setParent(Controller parent)
		{
			mParentController = parent;
		}*/

		public virtual Type GetEntityType()
		{
			return typeof(object);
		}
	}
}
