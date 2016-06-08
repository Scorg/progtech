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

namespace common {
	public class FormAPIBinding<TEntity, TValue>
		//where TDelegate: class
	{
		public enum Visibility : int {
			CreateEdit = 3,
			Create = 1,
			Edit = 2
		}

		public FormAPIBinding(string name, TableForm.ControlTypes ctltype, Func<TEntity, TValue> r, Action<TEntity, TValue> w, Func<TValue> dflt, Func<TValue, bool> valid=null)
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
		public Action<TEntity, TValue> Write { get; set; }
		public Func<TValue> Default { get; set; }
		public Func<TValue, bool> Validate { get; set; }
		public TableForm.ControlTypes ControlType { get; private set; }

		public Visibility Visiblity { get; set; }
	}


	//------------------------------------------------------------------------
	// GenericAPIController
	//------------------------------------------------------------------------
	public class GenericAPIController<TEntity, TKey> : Controller<DbContext>
		//where TContext : DbContext, new()
		where TEntity : class, new() {
		PagedList mList;
		TabForm mForm;

		protected bool mEditMode;
		bool mUseCache;
		protected bool mOwnCache;

		protected List<string> mEntityPKNames;
		protected DbContext mCache;
		protected TEntity	mSelectedEntity;

		Dictionary<string, ListLambdaBinding<TEntity, object>> mListBindings;
		Dictionary<string, FormAPIBinding<TEntity, object>> mFormBindings;


		public GenericAPIController(Control parent = null)
			: base(parent)
		{
			mListBindings = new Dictionary<string, ListLambdaBinding<TEntity, object>>();
			mFormBindings = new Dictionary<string, FormAPIBinding<TEntity, object>>();

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

			mEntityPKNames = new List<string>();//Utils.GetEntityPKNames<TContext, TEntity>(new TContext());

			ListPredicate = null;
			ListOrder = null;
			CustomSave = null;

			OrderAscending = true;

			//SetupForm();
			//SetupDataGrid();
			SubscribeHandlers();
		}

		~GenericAPIController()
		{
			UnsubscribeHandlers();
			mList.Parent = null;
			mForm.Parent = null;
		}

		public Expression<Func<TEntity, bool>> ListPredicate { get; set; }
		public Expression<Func<TEntity, object>> ListOrder { get; set; }
		//public Func<TEntity, ICollection<TEntity>> ListSelector { get; set; }
		public Func<ICollection<TEntity>, bool> ValidateList { get; set; }
		public Func<TableForm, ICollection<TEntity>, bool, bool> ValidateForm { get; set; }
		public Action<TableForm, TEntity> CustomSave { get; set; }
		public Action<TableForm> CustomDefault { get; set; }
		public Action<TableForm, TEntity> CustomLoad { get; set; }

		public Func<int, int, ICollection<TEntity>> GetList { get; set; }
		public Func<int> GetListCount { get; set; }
		public Func<TKey, TEntity> GetEntity { get; set; }
		public Func<TEntity> CreateEntity { get; set; }
		public Action<TEntity> InsertEntity { get; set; }
		public Action<TEntity> UpdateEntity { get; set; }
		public Action<TEntity> DeleteEntity { get; set; }
		public Action<TKey> DeleteByKey { get; set; }
		public Func<TEntity, TKey> KeySelector { get; set; }


		public string ParentCollection { get; set; }

		public Dictionary<string, ListLambdaBinding<TEntity, object>> ColumnBindings
		{
			get { return mListBindings; }
		}

		public Dictionary<string, FormAPIBinding<TEntity, object>> FormBindings
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

		public DbContext Cache
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

		public void SetupForm(FormAPIBinding<TEntity, object>.Visibility vis = FormAPIBinding<TEntity,object>.Visibility.CreateEdit)
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
		protected override void OnParentFormShowing(DbContext ctx)
		{
			if (Parent==null) throw new InvalidOperationException("Эта функция должна вызываться только при наличии родительского контроллера");

			/*if (mCache!=null && mOwnCache) {
				mCache.Dispose();
			}

			mCache = ctx;
			mOwnCache = false;*/

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
			if (ValidateList!=null) valid = valid && ValidateList(c);

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
			if (mSelectedEntity!=null) {
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

			//using (TContext model=new TContext()) {
				try {
					if (GetListCount!=null) count = GetListCount();//(Parent!=null ? Parent.GetCollection<TEntity>(ParentCollection).AsQueryable() : model.Set<TEntity>()).LongCount(ListPredicate);
				} catch (Exception ex) {
					ex.Log();
				}
			//}

			if (mList.InvokeRequired)
				mList.Invoke(new Action(() => mList.TotalCount = (int)count));
			else mList.TotalCount = (int)count;

			LoadCurrentPage();
		}

		protected void LoadCurrentPage()
		{
			mList.DataGrid.Rows.Clear();

			try {
				IEnumerable<TEntity> src = Parent!=null 
					? Parent.GetCollection<TEntity>(ParentCollection).Skip(mList.CurrentPage*mList.PageSize).Take(mList.PageSize) 
					: (GetList!=null ? GetList(mList.CurrentPage * mList.PageSize, mList.PageSize) : new TEntity[0]);

				LoadCurrentPage(src);
			} catch (Exception ex) {
				MessageBox.Show(ex.Message);
				ex.Log();
			}
		}

		// Загружает 
		protected void LoadCurrentPage(IEnumerable<TEntity> source)
		{
			if (source==null) throw new ArgumentNullException("Ссылка на источник равна Null");

			//Utils.GetEntityPKNames<TContext, TEntity>(model);
			//IQueryable<TEntity> q = source.Where(ListPredicate);
			//q = OrderAscending ? q.OrderBy(ListOrder) : q.OrderByDescending(ListOrder);

			foreach (TEntity ent in source) {
				int i = mList.DataGrid.Rows.Add();
				DataGridViewRow row = mList.DataGrid.Rows[i];

				row.HeaderCell.Value = Parent==null ? (object)KeySelector(ent) : ent;

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
			SetupForm(FormAPIBinding<TEntity, object>.Visibility.Create);

			// По идее эта функция не должна вызываться, когда mCache!=null и mOwnCache==true,
			// то есть когда контекст был нами открыт
			/*if (mCache==null) {
				mCache = new TContext();
				mCache.Configuration.AutoDetectChangesEnabled = true;
				mOwnCache = true;
			}*/

			mEditMode = false;
			mSelectedEntity = CreateEntity();//mCache.Set<TEntity>().Create();
			//mCache.Set<TEntity>().Add(mSelectedEntity);

			if (mSelectedEntity==null) throw new NullReferenceException("CreateEntity() вернуло null");

			//if (mCache==null)
			//	mEntityKey = Utils.GetEntityPK(mCache, mSelectedEntity);//new object[mEntityPKNames.Count];

			foreach (var pair in mFormBindings.Where(x => (x.Value.Visiblity & FormAPIBinding<TEntity, object>.Visibility.Create)>0)) {
				if (pair.Value.Default==null) continue;

				mForm.SetValue(pair.Key, pair.Value.Default());
			}

			if (CustomDefault!=null) CustomDefault(mForm.TableForm);

			//Дочерние контроллеры
			ShowChildren(mCache);
		}

		protected void ShowForm(int rowid)
		{
			if (mParent==null) return;

			mParent.Controls.Clear();
			mParent.Controls.Add(mForm);

			SetupForm(FormAPIBinding<TEntity, object>.Visibility.Edit);

			// По идее эта функция не должна вызываться, когда mCache!=null и mOwnCache==true,
			// то есть когда контекст был нами открыт.
			/*bool bCacheIsNew = mCache==null; //хаки такие хаки
			if (mCache==null) {
				mCache = new TContext();
				mCache.Configuration.AutoDetectChangesEnabled = true;
				mOwnCache = true;
			}*/

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

				//DbContext model = mCache;
				//model.Configuration.AutoDetectChangesEnabled = false;

				TEntity ent = null;

				// Если мы используем кэш, то у создаваемых записей ключи ещё неверные,
				// поэтому в ячейку строки сохраняем саму сущность
				if (Parent==null) {
					if (GetEntity == null) throw new NullReferenceException("GetEntity == null");

					ent = GetEntity((TKey)mList.DataGrid.Rows[rowid].HeaderCell.Value);
				} else {
					ent = (TEntity)mList.DataGrid.Rows[rowid].HeaderCell.Value;
				}

				if (ent!=null) {
					mEditMode = true;
					mSelectedEntity = ent;
					//mEntityKey = KeySelector(mSelectedEntity);//Utils.GetEntityPK<DbContext, TEntity>(model, mSelectedEntity);

					foreach (var binding in mFormBindings.Where(x => x.Value.Read!=null && (x.Value.Visiblity & FormAPIBinding<TEntity, object>.Visibility.Edit)>0)) {
						//if (binding.Value.Read==null) continue; //Или кинуть исключение?

						mForm.SetValue(binding.Key, binding.Value.Read(ent));
					}

					if (CustomLoad!=null) CustomLoad(Form.TableForm, SelectedEntity);
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
							DeleteEntity((TEntity)row.HeaderCell.Value);
						}
					} else {
						try {
							//using (TContext model=new TContext()) {
								foreach (DataGridViewRow row in mList.DataGrid.SelectedRows) {
									DeleteByKey((TKey)row.HeaderCell.Value);
								}

								//model.SaveChanges();
							//}
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

			FormAPIBinding<TEntity, object>.Visibility excludevis = mEditMode ? FormAPIBinding<TEntity, object>.Visibility.Create : FormAPIBinding<TEntity, object>.Visibility.Edit;

			bool valid = true;

			//if (ValidateForm==null) {
			valid = valid 
					&& (mFormBindings.Count==0 || mFormBindings.All(x => x.Value.Visiblity==excludevis || (x.Value.Validate==null || x.Value.Validate(mForm.GetValue(x.Key)))))
					&& (ValidateForm==null || ValidateForm(mForm.TableForm, Parent==null ? null : Parent.GetCollection<TEntity>(ParentCollection), mEditMode));
			//}

			if (!valid) {
				System.Media.SystemSounds.Beep.Play();
				return;
			}

			try {
				//DbContext model = mCache;
				TEntity ent = mSelectedEntity;

				// Сущность добавляется в контекст при открытии формы,
				// Но в коллекцию родителя мы должны добавить здесь
				if (!mEditMode) {
					if (Parent==null) {
						//model.Set<TEntity>().Add(ent);
					} else {
						Parent.GetCollection<TEntity>(ParentCollection).Add(ent);
					}
				}

				//Вытягиваем из формы данные и записываем их в сущность
				
				foreach (var binding in mFormBindings) {
					if (binding.Value.Write==null || binding.Value.Visiblity==excludevis) continue;

					binding.Value.Write(ent, mForm.GetValue(binding.Key));
				}

				if (CustomSave!=null) {
					CustomSave(mForm.TableForm, ent);
				}

				//mCache.ChangeTracker.DetectChanges();

				//Вызываем сохранение у дочерних контроллеров.
				if (!SaveChildren(ent)) return;

				if (mEditMode) {
					if (UpdateEntity==null) throw new NullReferenceException("Не задана функция обновления");

					UpdateEntity(mSelectedEntity);
				} else {
					if (InsertEntity==null) throw new NullReferenceException("Не задана функция добавления");

					InsertEntity(mSelectedEntity);
				}

				// Закрываем кэш
				/*if (mOwnCache) {
					mCache.SaveChanges();
					mCache.Dispose();
					mCache = null;
					mOwnCache = false;
				}*/

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
			if (mSelectedEntity==null) {
				throw new NullReferenceException("mCache == " + mCache + ", mSelectedEntity = " + mSelectedEntity);
			}

			ShowList();
		}

		protected void CancelEdit()
		{
			if (mCache==null) return;

			//Поскольку при окрытии формы сущность добавляется в конеткст (создание записи)
			// Удаляем только когда создаём запись и когда кэш не наш
			//if (!mOwnCache && !mEditMode)
			//	mCache.Set<TEntity>().Remove(mSelectedEntity);

			DiscardChildren();
			mSelectedEntity = null;
			mEntityKey = null;

			// Закрывать контекст необходимо после того, как дочерние контроллеры будут уведомлены
			/*if (mOwnCache) {
				mCache.Dispose();
			}*/

			mCache = null;
			mOwnCache = false;
		}

		protected void OnPageChange(object s, EventArgs e)
		{
			LoadCurrentPage();
		}

		public override ICollection<TE> GetCollection<TE>(string property, DbContext ctx)
		{
			if (mSelectedEntity==null)
				throw new InvalidOperationException("Была запрошена коллекция сущности, когда ни одна сущность не была выбрана");

			try {
				return (ICollection<TE>)typeof(TEntity).GetProperty(property).GetValue(mSelectedEntity);
			} catch (Exception ex) {
				ex.Log();
			}

			return null;// mCache.Entry<TEntity>(mSelectedEntity).Collection<TE>(property).CurrentValue;
		}

		public override IQueryable<TE> GetCollectionQuery<TE>(string property, DbContext ctx = null)
		{
			return ((ICollection<TE>)typeof(TEntity).GetProperty(property).GetValue(mSelectedEntity)).AsQueryable();
		}

		public override Type GetEntityType()
		{
			return typeof(TEntity);
		}
	}
}
