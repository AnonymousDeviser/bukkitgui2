﻿// OpListControl.cs in bukkitgui2/bukkitgui2
// Created 2014/08/29
// 
// This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0.
// If a copy of the MPL was not distributed with this file,
// you can obtain one at http://mozilla.org/MPL/2.0/.
// 
// ©Bertware, visit http://bertware.net

using System;
using System.Windows.Forms;
using MetroFramework.Forms;
using Net.Bertware.Bukkitgui2.MinecraftInterop.PlayerHandler;
using Net.Bertware.Bukkitgui2.MinecraftInterop.ServerConfig;

namespace Net.Bertware.Bukkitgui2.AddOn.Editor
{
	public partial class OpListControl : IAddonTab
	{
		public OpListControl()
		{
			InitializeComponent();
		}

		public IAddon ParentAddon { get; set; }

		private void WhitelistControl_Load(object sender, EventArgs e)
		{
			RefreshList();
		}

		private void RefreshList()
		{
			slvList.Items.Clear();
			foreach (ServerListItem item in ServerList.Ops.List.Values)
			{
				string[] content = {item.Name, item.Uuid, item.OpLevel.ToString()};
				ListViewItem lvi = new ListViewItem(content) {Tag = item.Name};
				slvList.Items.Add(lvi);
			}
		}

		private void btnAdd_Click(object sender, EventArgs e)
		{
			string name = MetroPrompt.ShowPrompt("Op player", "Enter the name of the player you'd like to op");
			if (string.IsNullOrEmpty(name)) return;

			PlayerActions.SetPlayerOp(name, true);

			RefreshList();
		}

		private void btnRemove_Click(object sender, EventArgs e)
		{
			if (slvList.SelectedItems.Count < 1) return;
			string name = slvList.SelectedItems[0].Tag.ToString();
			if (!string.IsNullOrEmpty(name)) PlayerActions.SetPlayerOp(name, false);
			RefreshList();
		}

		private void bntRefresh_Click(object sender, EventArgs e)
		{
			RefreshList();
		}

		private void OpListControl_VisibleChanged(object sender, EventArgs e)
		{
			if (Visible) RefreshList();
		}
	}
}