﻿#region License Header

// /***************************************************************************
//  *   Copyright (c) 2011 OpenUO Software Team.
//  *   All Right Reserved.
//  *
//  *   Program.cs
//  *
//  *   This program is free software; you can redistribute it and/or modify
//  *   it under the terms of the GNU General Public License as published by
//  *   the Free Software Foundation; either version 3 of the License, or
//  *   (at your option) any later version.
//  ***************************************************************************/

#endregion

#region Usings

using System;
using System.Windows.Forms;
using OpenUO.Core.Diagnostics;
using OpenUO.Core.Diagnostics.Tracing.Listeners;
using OpenUO.Core.Patterns;
using OpenUO.Ultima;
using OpenUO.Ultima.Windows.Forms;

#endregion

namespace Ultima.Winforms.Sample
{
    internal static class Program
    {
        /// <summary>
        ///     The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
#if DEBUG
            new DebugOutputEventListener();
#endif
            var container = new Container();

            container.RegisterModule<OpenUOCoreModule>();
            container.RegisterModule<OpenUOBitmapModule>();
            container.Register<SampleForm>();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(container.Resolve<SampleForm>());
        }
    }
}