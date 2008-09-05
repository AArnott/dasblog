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
using CookComputing.XmlRpc;

namespace newtelligence.DasBlog.Web.Services.MovableType
{
    /// <summary>
    /// 
    /// </summary>
    public struct Category
    {
        /// <summary>
        /// 
        /// </summary>
        public string categoryId;
        /// <summary>
        /// 
        /// </summary>
        [XmlRpcMissingMapping(MappingAction.Ignore)]
        public string categoryName;
        [XmlRpcMissingMapping(MappingAction.Ignore)]
        public bool isPrimary;
    }

    /// <summary>
    /// 
    /// </summary>
    public struct PostTitle
    {
        /// <summary>
        /// 
        /// </summary>
        [XmlRpcMember(Description="This is in the timezone of the weblog blogid.")]
        public DateTime dateCreated;
        /// <summary>
        /// 
        /// </summary>
        public string postid;
        /// <summary>
        /// 
        /// </summary>
        public string userid;
        /// <summary>
        /// 
        /// </summary>
        public string title;
    }

    /// <summary>
    /// 
    /// </summary>
    public struct TrackbackPing
    {
        /// <summary>
        /// 
        /// </summary>
        [XmlRpcMember(Description="The title of the entry sent in the ping.")]
        public string pingTitle;
        /// <summary>
        /// 
        /// </summary>
        [XmlRpcMember(Description="The URL of the entry.")]
        public string pingURL;
        /// <summary>
        /// 
        /// </summary>
        [XmlRpcMember(Description="The IP address of the host that sent the ping.")]
        public string pingIP;
    }

    /// <summary>
    /// 
    /// </summary>
    public struct TextFilter
    {
        /// <summary>
        /// 
        /// </summary>
        [XmlRpcMember(Description="unique string identifying a text formatting plugin")]
        public string key;
        /// <summary>
        /// 
        /// </summary>
        [XmlRpcMember(Description="readable description to be displayed to a user")]
        public string value;
    }

    /// <summary>
    /// 
    /// </summary>
    public interface IMovableType
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="blogid"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [XmlRpcMethod("mt.getCategoryList",
             Description="Returns a list of all categories defined in the weblog.")]
        [return: XmlRpcReturnValue(
                     Description="The isPrimary member of each Category structs is not used.")]
        Category[] mt_getCategoryList(
            string blogid,
            string username,
            string password);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="postid"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [XmlRpcMethod("mt.getPostCategories",
             Description="Returns a list of all categories to which the post is "
             + "assigned.")]
        Category[] mt_getPostCategories(
            string postid,
            string username,
            string password);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="blogid"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="numberOfPosts"></param>
        /// <returns></returns>
        [XmlRpcMethod("mt.getRecentPostTitles",
             Description="Returns a bandwidth-friendly list of the most recent "
             + "posts in the system.")]
        PostTitle[] mt_getRecentPostTitles(
            string blogid,
            string username,
            string password,
            int numberOfPosts);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="postid"></param>
        /// <returns></returns>
        [XmlRpcMethod("mt.getTrackbackPings",
             Description="Retrieve the list of TrackBack pings posted to a "
             + "particular entry. This could be used to programmatically "
             + "retrieve the list of pings for a particular entry, then "
             + "iterate through each of those pings doing the same, until "
             + "one has built up a graph of the web of entries referencing "
             + "one another on a particular topic.")]
        TrackbackPing[] mt_getTrackbackPings(
            string postid);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="postid"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [XmlRpcMethod("mt.publishPost",
             Description="Publish (rebuild) all of the static files related "
             + "to an entry from your weblog. Equivalent to saving an entry "
             + "in the system (but without the ping).")]
        [return: XmlRpcReturnValue(Description="Always returns true.")]
        bool mt_publishPost(
            string postid,
            string username,
            string password);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="postid"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="categories"></param>
        /// <returns></returns>
        [XmlRpcMethod("mt.setPostCategories",
             Description="Sets the categories for a post.")]
        [return: XmlRpcReturnValue(Description="Always returns true.")]
        bool mt_setPostCategories(
            string postid,
            string username,
            string password,
            [XmlRpcParameter(
                 Description="categoryName not required in Category struct.")]
            Category[] categories);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [XmlRpcMethod("mt.supportedMethods",
             Description="The method names supported by the server.")]
        [return: XmlRpcReturnValue(
                     Description="The method names supported by the server.")]
        string[] mt_supportedMethods();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        
        [XmlRpcMethod("mt.supportedTextFilters",
             Description="The text filters names supported by the server.")]
        [return: XmlRpcReturnValue(
                     Description="The text filters names supported by the server.")]
        TextFilter[] mt_supportedTextFilters();
    }
}
