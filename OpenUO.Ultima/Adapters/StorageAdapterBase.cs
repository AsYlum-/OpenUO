﻿#region License Header

// /***************************************************************************
//  *   Copyright (c) 2011 OpenUO Software Team.
//  *   All Right Reserved.
//  *
//  *   StorageAdapterBase.cs
//  *
//  *   This program is free software; you can redistribute it and/or modify
//  *   it under the terms of the GNU General Public License as published by
//  *   the Free Software Foundation; either version 3 of the License, or
//  *   (at your option) any later version.
//  ***************************************************************************/

#endregion

using System.Threading.Tasks;

using OpenUO.Core.Threading.Tasks;

namespace OpenUO.Ultima.Adapters
{
    public abstract class StorageAdapterBase : IStorageAdapter
    {
        public InstallLocation Install
        {
            get;
            set;
        }

        public abstract int Length
        {
            get;
        }

        public virtual void Initialize()
        {
            IsInitialized = true;
        }

        public Task InitializeAsync()
        {
            Initialize();
            return TaskResult.Complete;
        }

        public void Dispose()
        {
            Dispose(true);
        }

        public bool IsInitialized
        {
            get;
            private set;
        }

        protected virtual void Dispose(bool disposing)
        {
        }
    }
}