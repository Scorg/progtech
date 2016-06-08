using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace common {
	public class ControllerCollection<T>: List<Controller<T>> where T:class
	{
		Controller<T> mOwner;

		public ControllerCollection(Controller<T> owner)
			: base()
		{
			mOwner = owner;
		}

		public void Insert(int index, Controller<T> item)
		{
			if (item!=null && index>=0 && (index<=Count && item.Parent!=Owner || index<Count && item.Parent==Owner)) {
				//Вызываем удаление из контроллера-родителя
				item.Parent = null;

				//Вставляем в нашу коллекцию и уведомляем контроллер об этом
				base.Insert(index, item);
				item.Parent = Owner;
			}
		}

		public void RemoveAt(int index)
		{
			Controller<T> ctl = base[index];
			base.RemoveAt(index);
			ctl.Parent = null;
		}

		public Controller<T> this[int index]
		{
			get
			{
				return base[index];
			}
			set
			{
				if (base[index]!=value && value!=null) {
					Controller<T> old = base[index];
					base.RemoveAt(index);
					old.Parent = null;
				}
			}
		}

		public void Add(Controller<T> item)
		{
			if (!base.Contains(item)) {
				base.Add(item);
				//Уведомляем контроллер. Он удалит себя сам из списка прошлого родителя, если такой есть
				item.Parent = Owner;
			}
		}

		public void Clear()
		{
			List<Controller<T>> tmp = new List<Controller<T>>(this);
			base.Clear();
			tmp.ForEach(x => x.Parent=null);
		}

		public bool Contains(Controller<T> item)
		{
			return base.Contains(item);
		}

		public void CopyTo(Controller<T>[] array, int arrayIndex)
		{
			base.CopyTo(array, arrayIndex);
		}

		public int Count
		{
			get { return base.Count; }
		}

		public bool Remove(Controller<T> item)
		{
			if (base.Remove(item) && item.Parent==Owner) {
				item.Parent = null;

				return true;
			}

			return false;
		}

		public Controller<T> Owner { get { return mOwner; } }
	}
}
