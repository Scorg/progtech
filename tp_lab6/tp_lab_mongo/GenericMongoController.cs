using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data.Entity.Infrastructure;
using System.Data.Entity;
using MongoDB.Driver;
using MongoDB.Driver.Core;
using common;

namespace tp_lab.mongo {
	/*public struct ListLambdaBinding<TEntity, TValue>
		//where TDelegate: class
	{
		public ListLambdaBinding(string name, Func<TEntity, TValue> read, Action<TEntity, TValue> write = null, Func<TValue> validate = null)
			: this()
		{
			Name = name;
			Read = read;
			Write = write;
			Validate = validate;
		}

		public string Name { get; set; }

		public Func<TEntity, TValue> Read { get; set; }

		public Action<TEntity, TValue> Write { get; set; }

		public Func<TValue> Validate { get; set; }

		public bool AllowEdit
		{
			get { return Write!=null; }
		}
	}*/

	/*public class FormBinding<TContext, TEntity, TValue>
		//where TDelegate: class
	{
		public enum Visibility : int {
			CreateEdit = 3,
			Create = 1,
			Edit = 2
		}

		public FormBinding(string name, TableForm.ControlTypes ctltype, Func<TEntity, TValue> r, Action<TContext, TEntity, TValue> w, Func<TValue> dflt, Func<TValue, bool> valid=null)
		//	: this()
		{
			Name = name;
			Read = r;
			Write = w;
			Default = dflt;
			Validate = valid;
			ControlType = ctltype;

			Visiblity = Visibility.Create | Visibility.Edit;
		}

		public string Name { get; set; }
		public Func<TEntity, TValue> Read { get; set; }
		public Action<TContext, TEntity, TValue> Write { get; set; }
		public Func<TValue> Default { get; set; }
		public Func<TValue, bool> Validate { get; set; }
		public TableForm.ControlTypes ControlType { get; private set; }

		public Visibility Visiblity { get; set; }
	}
	*/

	//------------------------------------------------------------------------
	// GenericMongoController
	//------------------------------------------------------------------------
	public class GenericMongoController<TEntity, TKey> : Controller<MongoContext>
		//where MongoContext : DbContext, new()
		where TEntity : class, new() {
		PagedList mList;
		TabForm mForm;

		protected bool mEditMode;
		bool mUseCache;
		protected bool mOwnCache;

		protected List<string> mEntityPKNames;
		protected MongoContext mCache;
		protected TEntity	mSelectedEntity;

		Dictionary<string, ListLambdaBinding<TEntity, object>> mListBindings;
		Dictionary<string, FormBinding<MongoContext, TEntity, object>> mFormBindings;

		Expression<Func<TEntity, TKey>> mKeySelector;
		Func<TEntity, TKey> mCompiledKeySelector;


		public GenericMongoController(Control parent = null)
			: base(parent)
		{
			mListBindings = new Dictionary<string, ListLambdaBinding<TEntity, object>>();
			mFormBindings = new Dictionary<string, FormBinding<MongoContext, TEntity, object>>();

			mList = new PagedList();
			mForm = new TabForm();

			mList.Dock = DockStyle.Fill;
			mForm.Dock = DockStyle.Fill;

			mEditMode = false;
			mEntityKey = null;
			mSelectedEntity = null;

			mCache = null;// new List<TEntity>();
			mUseCache = false;
			mOwnCache = false;

			mEntityPKNames = null;//Utils.GetEntityPKNames<TEntity>(new MongoContext());

			ListPredicate = null;
			ListOrder = null;
			ListOrders = new List<Expression<Func<TEntity, object>>>();
			CustomSave = null;

			OrderAscending = true;

			//SetupForm();
			//SetupDataGrid();
			SubscribeHandlers();
		}

		~GenericMongoController()
		{
			UnsubscribeHandlers();
			mList.Parent = null;
			mForm.Parent = null;
		}

		public Expression<Func<TEntity, TKey>> KeySelector
		{
			get { return mKeySelector; }

			set
			{
				mKeySelector = value;
				if (value!=null) mCompiledKeySelector = value.Compile();
			}
		}

		public Expression<Func<TEntity, bool>> ListPredicate { get; set; }

		public Expression<Func<TEntity, object>> ListOrder { get; set; }
		public List<Expression<Func<TEntity, object>>> ListOrders { get; private set; }
		//public SortDefinition<TEntity> ListOrder { get; set; }

		//public Func<TEntity, ICollection<TEntity>> ListSelector { get; set; }

		public Func<MongoContext, ICollection<TEntity>, bool> ValidateList { get; set; }

		public Func<TableForm, MongoContext, bool, bool> ValidateForm { get; set; }

		public Action<TableForm, MongoContext, TEntity, bool> CustomSave { get; set; }

		public Action<TableForm, MongoContext> CustomDefault { get; set; }

		public Action<TableForm, MongoContext, TEntity> CustomLoad { get; set; }


		public string ParentCollection { get; set; }

		public Dictionary<string, ListLambdaBinding<TEntity, object>> ColumnBindings
		{
			get { return mListBindings; }
		}

		public Dictionary<string, FormBinding<MongoContext, TEntity, object>> FormBindings
		{
			get { return mFormBindings; }
		}

		public TabForm Form
		{
			get { return mForm; }
			protected set { mForm = value; }
		}

		public PagedList List
		{
			get { return mList; }
			protected set { mList = value; }
		}

		public MongoContext Cache
		{
			get { return mCache; }
		}

		public TEntity SelectedEntity
		{
			get { return mSelectedEntity; }
		}

		public bool OrderAscending { get; set; }

		public override void Show(Control parent)
		{
			if (parent!=null) mParent = parent;
			ShowList();
		}

		public void SetupForm()
		{
			mForm.ClearRows();

			foreach (var pair in mFormBindings) {
				mForm.AddRow(pair.Key, pair.Value.Name, pair.Value.ControlType);
			}
		}

		public void SetupForm(FormBinding<MongoContext, TEntity, object>.Visibility vis = FormBinding<MongoContext, TEntity,object>.Visibility.CreateEdit)
		{
			foreach (var pair in mFormBindings) {
				mForm.TableForm.SetVisible(pair.Key, (pair.Value.Visiblity & vis) > 0);
			}
		}

		public void SetupDataGrid()
		{
			DataGridView dg = mList.DataGrid;
			dg.Columns.Clear();
			dg.Rows.Clear();
			dg.ReadOnly = false;

			foreach (var pair in mListBindings) {
				int i = dg.Columns.Add(pair.Key, pair.Value.Name);
				var column = dg.Columns[i];

				column.ValueType = pair.Value.Read.Method.ReturnType;
				column.ReadOnly = !pair.Value.AllowEdit;
			}
		}

		protected virtual void SubscribeHandlers()
		{
			mList.AddItem += OnCreate;
			mList.EditItem += OnUpdate;
			mList.DeleteItems += OnDelete;
			mList.NextPage += OnPageChange;
			mList.PrevPage += OnPageChange;
			mList.PageNumberEntered += OnPageChange;
			mList.PageRefresh += OnPageChange;
			mList.PageSizeChanged += OnPageChange;

			mForm.SubscribeSave(OnConfirm);
			mForm.SubscribeCancel(OnCancel);
		}

		protected virtual void UnsubscribeHandlers()
		{
			mList.AddItem -= OnCreate;
			mList.EditItem -= OnUpdate;
			mList.DeleteItems -= OnDelete;
			mList.NextPage -= OnPageChange;
			mList.PrevPage -= OnPageChange;
			mList.PageNumberEntered -= OnPageChange;
			mList.PageRefresh -= OnPageChange;

			mForm.UnsubscribeSave(OnConfirm);
			mForm.UnsubscribeCancel(OnCancel);
		}


		// Вызывается, когда родительский контроллер открывает форму данных сущности (для создания или изменения)
		protected override void OnParentFormShowing(MongoContext ctx)
		{
			if (Parent==null) throw new InvalidOperationException("Эта функция должна вызываться только при наличии родительского контроллера");

			if (mCache!=null && mOwnCache) {
				mCache.Dispose();
			}

			mCache = ctx;
			mOwnCache = false;

			ShowList();
		}

		// Вызывается когда родительский контроллер сохраняет форму (редактирование/создание) или его родитель запросил сохранение (рекурсия)
		protected override bool OnParentFormSaving<T>(T parentEntity)
		{
			if (parentEntity == null) throw new ArgumentNullException();
			ICollection<TEntity> c = (ICollection<TEntity>)typeof(T).GetProperty(ParentCollection).GetValue(parentEntity);

			bool valid = true;

			// Если мы в это время находимся в состоянии открытой формы,
			// то нам тоже надо (?) сохранить изменения и попросить дочерние контроллеры тоже это сделать
			// Другой вариант - не сохранять изменения
			if (mSelectedEntity!=null) {
				// SaveForm() или OnConfirm() ?
				OnConfirm(mForm, new EventArgs()); // SaveChildren() вызывается внутри
			}

			// Если OnConfirm прошёл удачно, mSelectedEntity будет равно null
			valid = mSelectedEntity==null;

			// Проверяем список, т.е. всякие дела типа уникальности и т.п.
			if (ValidateList!=null) valid = valid && ValidateList(mCache, c);

			if (valid) mCache = null;
			return valid;
		}

		// Вызывается, когда родительский контроллер закрывает открытую форму без сохранения.
		// При этом в родительском контроллере должна быть выбрана сущность (mSelectedEntity) или её ключ (mEntityKey)
		protected override void OnParentFormDiscarding()
		{
			// Если наш контроллер находится в состоянии открытой формы (поля в условии показывают на это),
			// то мы тоже должны "закрыть" свою открытую форму (и так по цепочке родитель-потомок)
			//
			// ?? Надо ли вызвать здесь OnCancel()?
			// - Наверно нет, так как родитель закрывает форму, и показать свой список мы всё равно не сможем
			if (/*mEntityKey!=null || */mSelectedEntity!=null) {
				DiscardChildren();

				mSelectedEntity = null;
				mEntityKey = null;
			}

			// Забываем про создаваемые сущности, когда родительский контроллер отменяет создание новой
			mCache = null;
			//mUseCache = false;
		}

		public void ShowList()
		{
			if (mParent==null) return;

			CancelEdit();

			mParent.Controls.Clear();
			mParent.Controls.Add(mList);

			long count=0;

			using (var model = new MongoContext()) {
				try {
					count = (Parent!=null ? Parent.GetCollection<TEntity>(ParentCollection).AsQueryable() : model.Set<TEntity>().AsQueryable()).LongCount(ListPredicate);
				} catch (Exception ex) {
					ex.Log();
				}
			}

			if (mList.InvokeRequired)
				mList.Invoke(new Action(() => mList.TotalCount = (int)count));
			else mList.TotalCount = (int)count;

			LoadCurrentPage();
		}

		protected void LoadCurrentPage()
		{
			mList.DataGrid.Rows.Clear();

			try {
				using (MongoContext model=new MongoContext()) {
					// отладка
					//model.Database.Log = x => System.Diagnostics.Debug.WriteLine(x);

					LoadCurrentPage(Parent!=null ? Parent.GetCollection<TEntity>(ParentCollection).AsQueryable() : model.Set<TEntity>().AsQueryable());
				}
			} catch (Exception ex) {
				MessageBox.Show(ex.Message);
				ex.Log();
			}
		}

		// Загружает 
		protected void LoadCurrentPage(IQueryable<TEntity> source)
		{
			if (source==null) throw new ArgumentNullException("Ссылка на источник равна Null");
			if (Parent==null && mCompiledKeySelector==null) throw new NullReferenceException("Не указан выбиратель ключа сущности KeySelector");

			//Utils.GetEntityPKNames<MongoContext, TEntity>(model);
			IQueryable<TEntity> q = source.Where(ListPredicate);
			
			if (ListOrder!=null) { // Переклюк, поскольку Монго не поддерживает сортировку по вычисляемому полю
				q = OrderAscending ? q.OrderBy(ListOrder) : q.OrderByDescending(ListOrder);
			} else {
				foreach (var expr in ListOrders) {
					q = OrderAscending ? q.OrderBy(expr) : q.OrderByDescending(expr);
				}
			}
			

			foreach (TEntity ent in q.Skip(mList.CurrentPage * mList.PageSize).Take(mList.PageSize).ToList()) {
				int i = mList.DataGrid.Rows.Add();
				DataGridViewRow row = mList.DataGrid.Rows[i];

				row.HeaderCell.Value = Parent==null ? mCompiledKeySelector(ent) : (object)ent;

				foreach (var pair in mListBindings) {
					row.Cells[pair.Key].Value = pair.Value.Read(ent);
				}
			}
		}

		protected void ShowForm()
		{
			if (mParent==null) return;

			mParent.Controls.Clear();
			mParent.Controls.Add(mForm);

			//mForm.Reset();
			SetupForm(FormBinding<MongoContext, TEntity, object>.Visibility.Create);

			// По идее эта функция не должна вызываться, когда mCache!=null и mOwnCache==true,
			// то есть когда контекст был нами открыт
			if (mCache==null) {
				mCache = new MongoContext();
				//mCache.Configuration.AutoDetectChangesEnabled = true;
				mOwnCache = true;
			}

			mEditMode = false;
			mSelectedEntity = new TEntity();//mCache.Set<TEntity>().Create();
			//mCache.Set<TEntity>().Add(mSelectedEntity);

			//if (mCache==null)
			//	mEntityKey = Utils.GetEntityPK(mCache, mSelectedEntity);//new object[mEntityPKNames.Count];

			foreach (var pair in mFormBindings.Where(x => (x.Value.Visiblity & FormBinding<MongoContext, TEntity, object>.Visibility.Create)>0)) {
				if (pair.Value.Default==null) continue;

				mForm.SetValue(pair.Key, pair.Value.Default());
			}

			if (CustomDefault!=null) CustomDefault(mForm.TableForm, mCache);

			//Дочерние контроллеры
			ShowChildren(mCache);
		}

		protected void ShowForm(int rowid)
		{
			if (mParent==null) return;

			mParent.Controls.Clear();
			mParent.Controls.Add(mForm);

			SetupForm(FormBinding<MongoContext, TEntity, object>.Visibility.Edit);

			// По идее эта функция не должна вызываться, когда mCache!=null и mOwnCache==true,
			// то есть когда контекст был нами открыт.
			bool bCacheIsNew = mCache==null; //хаки такие хаки
			if (mCache==null) {
				mCache = new MongoContext();
				//mCache.Configuration.AutoDetectChangesEnabled = true;
				mOwnCache = true;
			}

			try {
				// Когда пользователь открывает форму для редактирования сущности,
				// мы можем как быть дочерним контроллером и находиться в режиме создания родительской сущности,
				// так и сами быть родителем (корнем).
				// Если мы - корень, то при редактирвоании мы не создаём кэш, а используем временный контекст (или всё-таки создавать кэш?).
				// При этом мы должны сообщать дочерним контроллерам, откуда им брать данные (связанные сущности).
				// Если же мы не являемся корнем, то...
				// --------------------------------
				// В любом случае, если мы - корень, мы должны открывать кэш, так как сохранять будем всё сразу.
				// Единственная(?) проблема - если дочерние контроллеры наплодят кучу записей (сущностей),
				// EF сожрёт кучу памяти и при сохранении будет жёстко тупить
				//
				// когда-то здесь был using ()

				MongoContext model = mCache;
				var filter = Builders<TEntity>.Filter.Eq(KeySelector, (TKey)mList.DataGrid.Rows[rowid].HeaderCell.Value);
				//model.Configuration.AutoDetectChangesEnabled = false;

				TEntity ent = null;

				// Если мы используем кэш, то у создаваемых записей ключи ещё неверные,
				// поэтому в ячейку строки сохраняем саму сущность
				if (bCacheIsNew) {
					ent = model.Set<TEntity>().Find(filter).FirstOrDefault();
				} else {
					ent = (TEntity)mList.DataGrid.Rows[rowid].HeaderCell.Value;
				}

				if (ent!=null) {
					mEditMode = true;
					mSelectedEntity = ent;
					//mEntityKey = mCompiledKeySelector(mSelectedEntity);

					foreach (var binding in mFormBindings.Where(x => x.Value.Read!=null && (x.Value.Visiblity & FormBinding<MongoContext, TEntity, object>.Visibility.Edit)>0)) {
						//if (binding.Value.Read==null) continue; //Или кинуть исключение?

						mForm.SetValue(binding.Key, binding.Value.Read(ent));
					}

					if (CustomLoad!=null) CustomLoad(Form.TableForm, Cache, SelectedEntity);
				}
			} catch (Exception ex) {
				//MessageBox.Show(ex.Message);
				ex.Log();
			}

			// Дочерние контроллеры
			// Если мы используем кэш, то говорим дочерним контроллерам тоже использовать
			ShowChildren(mCache);
		}

		protected void OnCreate(object s, EventArgs e)
		{
			ShowForm();
		}

		protected void OnUpdate(object s, EventArgs e)
		{
			if (mList.DataGrid.SelectedRows.Count==1) {
				ShowForm(mList.DataGrid.SelectedRows[0].Index);
			}
		}

		protected void OnDelete(object s, EventArgs e)
		{
			if (mList.DataGrid.SelectedRows.Count>0) {
				if (MessageBox.Show(String.Format("Удалить безвозвратно {0} записей?", mList.DataGrid.SelectedRows.Count), "ВНИМАНИЕ", MessageBoxButtons.YesNo) == DialogResult.Yes) {
					if (Parent!=null) {
						foreach (DataGridViewRow row in mList.DataGrid.SelectedRows) {
							Parent.GetCollection<TEntity>(ParentCollection).Remove((TEntity)row.HeaderCell.Value);
						}
					} else {
						try {
							using (MongoContext model=new MongoContext()) {
								foreach (DataGridViewRow row in mList.DataGrid.SelectedRows) {
									model.Set<TEntity>().DeleteMany(Builders<TEntity>.Filter.Eq(KeySelector, (TKey)row.HeaderCell.Value));
								}
							}
						} catch (Exception ex) {
							MessageBox.Show("Удаление не удалось");
							ex.Log();
						}
					}
				}
			}

			LoadCurrentPage();
		}

		// Вызывается нажатием на кнопку "сохранить" открытой формы
		// Здесь надо, по-идее, <- ???
		protected void OnConfirm(object s, EventArgs e)
		{
			if (mSelectedEntity==null) throw new NullReferenceException("Выбранная сущность равна null");

			FormBinding<MongoContext, TEntity, object>.Visibility excludevis = mEditMode ? FormBinding<MongoContext, TEntity, object>.Visibility.Create : FormBinding<MongoContext, TEntity, object>.Visibility.Edit;

			bool valid = true;

			//if (ValidateForm==null) {
			valid = valid 
						&& (mFormBindings.Count==0 || mFormBindings.All(x => x.Value.Visiblity==excludevis || (x.Value.Validate==null || x.Value.Validate(mForm.GetValue(x.Key)))))
						&& (ValidateForm==null || ValidateForm(mForm.TableForm, mCache, mEditMode));
			//}

			if (!valid) {
				System.Media.SystemSounds.Beep.Play();
				return;
			}

			try {
				//MongoContext model = mCache;
				TEntity ent = mSelectedEntity;

				// Сущность добавляется в контекст при открытии формы,
				// Но в коллекцию родителя мы должны добавить здесь
				if (!mEditMode) {
					if (Parent==null) {
						//model.Set<TEntity>().Add(ent);
					} else {
						//Parent.GetCollection<TEntity>(ParentCollection).Add(ent);
					}
				}

				//Вытягиваем из формы данные и записываем их в сущность
				foreach (var binding in mFormBindings) {
					if (binding.Value.Write==null || binding.Value.Visiblity==excludevis) continue;

					binding.Value.Write(mCache, ent, mForm.GetValue(binding.Key));
				}

				if (CustomSave!=null)
					CustomSave(mForm.TableForm, mCache, ent, mEditMode);



				//Вызываем сохранение у дочерних контроллеров.
				if (!SaveChildren(ent)) return;

				// Сохраняем и закрываем кэш
				if (mOwnCache) {
					if (mEditMode)
						mCache.Set<TEntity>().ReplaceOne(Builders<TEntity>.Filter.Eq(KeySelector, mCompiledKeySelector(mSelectedEntity)), mSelectedEntity);
					else
						mCache.Set<TEntity>().InsertOne(mSelectedEntity);

					mCache.Dispose();
					mCache = null;
					mOwnCache = false;
				} else { // Сохраняем в коллекцию внутри документа
					if (!mEditMode)
						Parent.GetCollection<TEntity>(ParentCollection).Add(mSelectedEntity);
				}

				mSelectedEntity = null;
				mEntityKey = null;

				ShowList();
			} catch (System.Data.Entity.Validation.DbEntityValidationException ex) {
				//MessageBox.Show(ex.Message + " " + string.Join(", ", ex.EntityValidationErrors.Select(x => string.Join(", ", x.ValidationErrors.Select(y => y.ErrorMessage + ": " + y.PropertyName)))));
				ex.Log();
			} /*catch() {
			}*/ catch (Exception ex) {
				ex.Log();

				System.Media.SystemSounds.Beep.Play();
				MessageBox.Show("Сохранение не удалось");
			}
		}

		protected void OnCancel(object s, EventArgs e)
		{
			// Отмена должна вызываться формой (в идеальном случае)
			// - при редактировании или создании сущности.
			// ПРи этом всегда должен быть открыт временный контекст
			if (mSelectedEntity==null || mCache==null) {
				throw new NullReferenceException("mCache == " + mCache + ", mSelectedEntity = " + mSelectedEntity);
			}

			ShowList();
		}

		protected void CancelEdit()
		{
			if (mSelectedEntity==null) return;

			//Поскольку при окрытии формы сущность добавляется в конеткст (создание записи)
			// Удаляем только когда создаём запись и когда кэш не наш
			//if (!mOwnCache && !mEditMode)
			//	mCache.Set<TEntity>().Remove(mSelectedEntity);

			DiscardChildren();
			mSelectedEntity = null;
			mEntityKey = null;

			// Закрывать контекст необходимо после того, как дочерние контроллеры будут уведомлены
			if (mOwnCache) {
				mCache.Dispose();
				mCache = null;
			}

			mOwnCache = false;
		}

		protected void OnPageChange(object s, EventArgs e)
		{
			LoadCurrentPage();
		}

		public override ICollection<TE> GetCollection<TE>(string property, MongoContext ctx = null)
		{
			if (mCache == null)
				throw new NullReferenceException("Нельзя вызывать этот метод без указания контекста, когда у контроллера нет кэша");
			if (mSelectedEntity==null)
				throw new InvalidOperationException("Была запрошена коллекция сущности, когда ни одна сущность не была выбрана");

			try {
				return (ICollection<TE>)GetEntityType().GetProperty(property).GetValue(mSelectedEntity);
			} catch (Exception ex) {
				ex.Log();
			}

			return (ICollection<TE>)GetEntityType().GetProperty(property).GetValue(mSelectedEntity);
		}

		public override IQueryable<TE> GetCollectionQuery<TE>(string property, MongoContext ctx = null)
		{
			return ((ICollection<TE>)GetEntityType().GetProperty(property).GetValue(mSelectedEntity)).AsQueryable();
		}

		public override Type GetEntityType()
		{
			return typeof(TEntity);
		}
	}
}
