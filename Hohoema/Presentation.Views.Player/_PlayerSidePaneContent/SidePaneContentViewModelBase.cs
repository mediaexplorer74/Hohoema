﻿using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Disposables;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Hohoema.Presentation.ViewModels.Player.PlayerSidePaneContent
{
	public abstract class SidePaneContentViewModelBase : ObservableObject, IDisposable
	{
        private static readonly CompositeDisposable compositeDisposable = new CompositeDisposable();
        protected ICollection<IDisposable> _CompositeDisposable = compositeDisposable;

		public SidePaneContentViewModelBase()
		{
		}

        virtual public Task OnEnter() { return Task.CompletedTask; }
        virtual public void OnLeave() { }

		public virtual void Dispose()
		{
			compositeDisposable?.Dispose();
		}
	}
}
