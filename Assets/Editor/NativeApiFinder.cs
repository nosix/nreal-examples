using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using UnityEditor;

namespace Editor
{
    internal class NativeApi
    {
        private readonly Dictionary<string, DllEntry> _dll = new Dictionary<string, DllEntry>();

        public void Add(string dllName, string fullName, string api)
        {
            if (!_dll.TryGetValue(dllName, out var dll))
            {
                dll = new DllEntry();
                _dll[dllName] = dll;
            }
            dll.Add(fullName, api);
        }

        public void WriteTo(StreamWriter writer)
        {
            var sortedDllNames = _dll.Keys.ToList();
            sortedDllNames.Sort();

            foreach (var dllName in sortedDllNames)
            {
                writer.WriteLine($"## {dllName}");
                writer.WriteLine();

                _dll[dllName].WriteTo(writer);
            }
        }
    }

    internal class DllEntry
    {
        private static readonly Regex ApiRegex = new Regex(@"(.+)\((.*)\)");

        private readonly Dictionary<string, List<string>> _apis = new Dictionary<string, List<string>>();

        public void Add(string fullName, string api)
        {
            if (!_apis.TryGetValue(fullName, out var apiList))
            {
                apiList = new List<string>();
                _apis[fullName] = apiList;
            }
            apiList.Add(api);
        }

        public void WriteTo(StreamWriter writer)
        {
            var sortedFullNames = _apis.Keys.ToList();
            sortedFullNames.Sort();

            foreach (var fullName in sortedFullNames)
            {
                writer.WriteLine($"### {fullName.Split('/').Last()}");
                writer.WriteLine();

                var apis = _apis[fullName];
                foreach (var api in apis)
                {
                    var m = ApiRegex.Match(api);
                    if (m.Success)
                    {
                        writer.WriteLine($"- {m.Groups[1].Value}");
                        var parameters = m.Groups[2].Value.Split(',')
                            .Select(p => p.Trim())
                            .Where(p => p.Length != 0);
                        foreach (var p in parameters) writer.WriteLine($"    - {p}");
                    }
                    else
                    {
                        writer.WriteLine($"`{api}`");
                    }
                    writer.WriteLine();
                }
            }
        }
    }

    public static class NativeApiFinder
    {
        private static readonly Regex DllApiRegex = new Regex(@"\[DllImport\((.+?)\)\]\s+((?:.|\s)+?);");

        [MenuItem("Assets/Extract Native Api")]
        private static void ExtractNativeApi()
        {
            var nativeApi = new NativeApi();

            var dirStack = new Stack<DirectoryInfo>();
            foreach (var guid in Selection.assetGUIDs)
            {
                var path = AssetDatabase.GUIDToAssetPath(guid);
                var dirInfo = new DirectoryInfo(path);
                if (!dirInfo.Exists) continue;
                dirStack.Push(dirInfo);
            }

            while (dirStack.Count > 0)
            {
                var dirInfo = dirStack.Pop();

                var scriptFiles = dirInfo.GetFiles("*.cs");
                foreach (var scriptFile in scriptFiles) ExtractDllImport(scriptFile, nativeApi);
                foreach (var dir in dirInfo.GetDirectories()) dirStack.Push(dir);
            }

            using (var writer = new StreamWriter("native-api.md", false, Encoding.UTF8))
            {
                writer.WriteLine("# Native API");
                writer.WriteLine();
                nativeApi.WriteTo(writer);
            }
        }

        private static void ExtractDllImport(FileInfo file, NativeApi nativeApi)
        {
            using (var reader = file.OpenText())
            {
                var matches = DllApiRegex.Matches(reader.ReadToEnd());
                foreach (Match m in matches)
                {
                    var dllName = m.Groups[1].Value.Split(',').First()
                        .Replace("\"", "")
                        .Replace(".dll", "");
                    var api = m.Groups[2].Value
                        .Replace("public", "")
                        .Replace("static", "")
                        .Replace("extern", "");
                    api = Regex.Replace(api, @"\s+", " ");
                    api = api.Trim();
                    nativeApi.Add(dllName, file.FullName, api);
                }
            }
        }
    }
}