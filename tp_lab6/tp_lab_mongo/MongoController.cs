using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using common;

namespace tp_lab {
	public class MongoController : Controller {
		//protected Control mParent;
		//protected object[] mEntityKey;

		MongoController mParentController;
		ControllerCollection<MongoController> mChildControllers;

		public MongoController(Control parent = null)
			: base(parent)
		{
			//mParent = parent;
			//mParentController=null;
			//mChildControllers=new ControllerCollection(this);
		}

		public virtual void Show(Control parent=null)
		{
		}

		protected virtual void OnParentFormShowing(DbContext ctx) { }
		protected virtual bool OnParentFormValidating() { return true; }
		protected virtual bool OnParentFormSaving<TEntity>(TEntity entity) { return true; }
		protected virtual void OnParentFormDiscarding(/* передать что-то? */) { }

		protected virtual void ShowChildren(MongoContext ctx)
		{
			foreach (Controller ctl in Controllers) {
				ctl.OnParentFormShowing(ctx);
			}
		}

		protected virtual bool ValidateChildren()
		{
			bool res = true;
			foreach (Controller ctl in Controllers) {
				res = res && ctl.OnParentFormValidating();
			}
			return res;
		}

		protected virtual bool SaveChildren<T>(T ent)
		{
			bool res = true;
			foreach (Controller ctl in Controllers) {
				res = res && ctl.OnParentFormSaving<T>(ent);
			}
			return res;

		}

		protected virtual void DiscardChildren()
		{
			foreach (Controller ctl in Controllers) {
				ctl.OnParentFormDiscarding();
			}
		}

		public virtual ControllerCollection Controllers
		{
			get { return mChildControllers; }
		}

		public virtual Controller Parent
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

		public virtual ICollection<TEntity> GetCollection<TEntity>(string property) where TEntity : class
		{
			return null;
		}

		public virtual IQueryable<TEntity> GetCollectionQuery<TEntity>(string property) where TEntity : class
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
