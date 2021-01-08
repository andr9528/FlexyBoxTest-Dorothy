using System;
using System.Collections.Generic;
using System.Text;

namespace Dorothy.Core.Enums
{
    public static class Target 
    {
        public static string GetSearchTarget(SearchTarget target) 
        {
            if (target == SearchTarget.Null) throw new NullReferenceException($"{nameof(target)} cannot be Null");
            SearchTargetDictionary.TryGetValue(target, out var value);
            return value;
        }

        public static Dictionary<SearchTarget, string> SearchTargetDictionary { get; } = new Dictionary<SearchTarget, string>()
        {
            { SearchTarget.All, "Web, Files And Folders"},
            { SearchTarget.WebAndFiles, "Web And Files"},
            { SearchTarget.WebAndFolder, "Web And Folder"},
            { SearchTarget.FilesAndFolders, "Files And Folders"},
            { SearchTarget.Web, "Web"},
            { SearchTarget.Files, "Files"},
            { SearchTarget.Folders, "Folders"},
        };
    }

    public enum SearchTarget
    {
        Null, All, WebAndFiles, WebAndFolder, FilesAndFolders, Web, Files, Folders
    }
}
