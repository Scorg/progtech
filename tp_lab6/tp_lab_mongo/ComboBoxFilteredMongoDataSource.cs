using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Windows.Forms;
using System.Timers;
using System.Data.Entity;
using MongoDB.Driver;
using common;

namespace tp_lab.mongo {
	public class ComboBoxFilteredMongoDataSource<TEntity, TKey> : BindingSource, IRefreshable//BindingList<KeyValuePair<TKey, string>>
		//where TContext : DbContext, new()
		where TEntity : class
		where TKey : struct {
		ComboBox mComboBox;
		System.Timers.Timer mTimer;
		bool bNeedUpdate,
			 bRefreshing;

		List<KeyValuePair<TKey, string>> mList;

		string mFilter;
		int mLimit;

		public ComboBoxFilteredMongoDataSource(ComboBox cb, Func<TEntity, TKey> keyselector, Func<TEntity, string> valueselector, double delay, bool nullable, bool casesensitive=false, Expression<Func<TEntity, bool>> predicate = null, Func<MongoContext, IQueryable<TEntity>> srcSelector = null, bool load=true)
			: base()
		{
			if (cb==null) throw new ArgumentNullException();

			mList = new List<KeyValuePair<TKey, string>>();
			base.DataSource = mList;
			base.DataMember = null;

			NullKey = default(TKey);
			NullValue = "(нет)";

			mComboBox = cb;
			//mComboBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
			//mComboBox.AutoCompleteSource = AutoCompleteSource.ListItems;
			mComboBox.ValueMember = "Key";
			mComboBox.DisplayMember = "Value";
			mComboBox.DataSource = this;
			mComboBox.TextChanged += mComboBox_TextChanged;
			mComboBox.TextUpdate +=mComboBox_TextUpdate;
			mComboBox.KeyPress += mComboBox_KeyPress;

			IsNullable = nullable;
			CaseSensitive = casesensitive;
			KeySelector = keyselector;
			ValueSelector = valueselector;
			Predicate = predicate;
			CollectionSelector = srcSelector;

			mTimer = new System.Timers.Timer();
			mTimer.Interval = delay;
			mTimer.SynchronizingObject = mComboBox;
			mTimer.Elapsed += mTimer_Elapsed;
			

			mFilter = "";
			mLimit = int.MaxValue;
			bNeedUpdate = false;
			bRefreshing = false;

			if (load)
				Refresh();
		}

		void mComboBox_TextUpdate(object sender, EventArgs e)
		{

		}

		void mComboBox_KeyPress(object sender, KeyPressEventArgs e)
		{
			bNeedUpdate = e.KeyChar!='\b';
		}

		void mComboBox_TextChanged(object sender, EventArgs e)
		{
			mTimer.Stop();

			// стартуем только если текст писали, а не стирали
			if (!bRefreshing && bNeedUpdate) {
				mFilter = mComboBox.Text;

				//if ()
					mTimer.Start();

				//System.Media.SystemSounds.Asterisk.Play();

				//((Label)mComboBox.Parent.Controls[mComboBox.Name+"_lbl"]).Text = mFilter;
				bNeedUpdate = false;
			}

		}

		void mTimer_Elapsed(object sender, ElapsedEventArgs e)
		{
			mTimer.Stop();
			Refresh();
		}

		public void Refresh()
		{
			bRefreshing = true;

			using (MongoContext ctx = new MongoContext()) {
				try {
					List<KeyValuePair<TKey, string>> list = mList;// new List<KeyValuePair<TKey, string>>();

					base.SuspendBinding();
					mComboBox.BeginUpdate();

					list.Clear();

					if (IsNullable) {
						list.Add(new KeyValuePair<TKey, string>(NullKey, NullValue));
					}

					string filterstring = mFilter.ToLower();
					TKey? lastvalue = (TKey?)mComboBox.SelectedValue;

					IQueryable<TEntity> src = CollectionSelector==null ? ctx.Set<TEntity>().AsQueryable() : CollectionSelector(ctx);
					IQueryable<TEntity> prefiltered = Predicate!=null ? src.Where(Predicate) : src;

					list.AddRange(prefiltered.AsEnumerable().Where(x => ValueSelector(x).ToLower().StartsWith(filterstring)).OrderBy(ValueSelector)/*.AsEnumerable()*/.Select(x => new KeyValuePair<TKey, string>(KeySelector(x), ValueSelector(x))).Take(mLimit)/*.ToList()*/);
					
					//base.DataSource = list;
					//base.DataMember = null;
					base.ResumeBinding();
					//base.ResetBindings(false);
					//base.OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
					//mComboBox.DisplayMember = "Value";


					if (lastvalue!=null && list.Any(x => lastvalue.Value.Equals(x.Key))) {
						mComboBox.SelectedValue = lastvalue;
					} else if (IsNullable ? list.Count>1 : list.Count>0) {
						mComboBox.SelectedIndex = 0;
					} else {
						mComboBox.SelectedIndex = -1;
						mComboBox.Text = filterstring;
					}

					mComboBox.SelectionStart = filterstring.Length;
					mComboBox.SelectionLength = mComboBox.Text.Length - filterstring.Length;
					//mComboBox.DroppedDown = true;

					//mComboBox.Invoke(new Action(() => { mComboBox.Refresh(); }));
					mComboBox.EndUpdate();
				} catch (Exception ex) {
					ex.Log();
				}
			}

			bRefreshing = false;
		}

		public Func<TEntity, TKey> KeySelector { get; set; }
		public Func<TEntity, string> ValueSelector { get; set; }
		public Func<MongoContext, IQueryable<TEntity>> CollectionSelector { get; set; }
		public Expression<Func<TEntity, bool>> Predicate { get; set; }

		public bool IsNullable { get; set; }
		public bool CaseSensitive { get; set; }
		public TKey NullKey { get; set; }
		public string NullValue { get; set; }
		public int MaxItems
		{
			get { return mLimit; }
			set { if (value>0) mLimit = value; }
		}
	}
}
