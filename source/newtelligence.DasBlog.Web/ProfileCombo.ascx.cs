#region Copyright (c) 2003, newtelligence AG. All rights reserved.
/*
// Copyright (c) 2003, newtelligence AG. (http://www.newtelligence.com)
// Original BlogX Source Code: Copyright (c) 2003, Chris Anderson (http://simplegeek.com)
// All rights reserved.
//  
// Redistribution and use in source and binary forms, with or without modification, are permitted 
// provided that the following conditions are met: 
//  
// (1) Redistributions of source code must retain the above copyright notice, this list of 
// conditions and the following disclaimer. 
// (2) Redistributions in binary form must reproduce the above copyright notice, this list of 
// conditions and the following disclaimer in the documentation and/or other materials 
// provided with the distribution. 
// (3) Neither the name of the newtelligence AG nor the names of its contributors may be used 
// to endorse or promote products derived from this software without specific prior 
// written permission.
//      
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY EXPRESS 
// OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY 
// AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR 
// CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL 
// DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, 
// DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER 
// IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT 
// OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
// -------------------------------------------------------------------------
//
// Original BlogX source code (c) 2003 by Chris Anderson (http://simplegeek.com)
// 
// newtelligence is a registered trademark of newtelligence Aktiengesellschaft.
// 
// For portions of this software, the some additional copyright notices may apply 
// which can either be found in the license.txt file included in the source distribution
// or following this notice. 
//
*/
#endregion

using System;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Caching;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using newtelligence.DasBlog.Web.Core;
using newtelligence.DasBlog.Web;
using System.Collections.Generic;

namespace newtelligence.DasBlog.Web
{	
	public partial class ProfileCombo : UserControl
	{

		internal class UserSorter : IComparer<User>
		{
            /// <summary>
            /// Creates a new default instance of the <see cref="UserSorter" /> class.
            /// </summary>
            public UserSorter() { }

			public int Compare(User x, User y)
			{
				return string.Compare(GetName(x), GetName(y));
			}

            private string GetName(User user)
            {
                if (user == null) { return null; }

                return String.IsNullOrEmpty(user.DisplayName) ? user.Name : user.DisplayName;
            }
		}

		protected void Page_Load(object sender, EventArgs e)
		{						
			SharedBasePage page = this.Page as SharedBasePage;

			if (!page.SiteConfig.EnableStartPageCaching)
			{
				UserCollection users = SiteSecurity.GetSecurity().Users;
												
				if (users != null && users.Count > 0)
				{					
					listProfiles.Items.Add(new ListItem("(select)", string.Empty));

					users.Sort(new UserSorter());

					foreach(User user in users)
					{										
						string profileName = string.Empty;

						if (user.DisplayName != null && user.DisplayName.Length > 0)
							profileName = user.DisplayName;
						else profileName = user.Name;
								
						listProfiles.Items.Add(new ListItem(profileName, user.Name));
					}
				}
				else
				{
					this.Visible = false;
				}
			}
			else
			{
				this.Visible = false;
			}			

			listProfiles.SelectedValue = string.Empty;
		}

		protected void listProfiles_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			SharedBasePage page = Page as SharedBasePage;
			string profileName = listProfiles.SelectedValue;
			if (profileName != null && profileName != string.Empty)
			{
				page.Redirect(string.Format("~/Profile.aspx?user={0}", profileName));
			}			
		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{

		}
		#endregion

	}
}
