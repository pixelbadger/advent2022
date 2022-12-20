using System.Text;

namespace Advent2022
{
    public class Day7
    {
        private readonly string _input;

        private Directory _rootDirectory;
        private Directory _parentDirectory;
        private Directory _currentDirectory;

        public Day7(string inputFilePath)
        {
            _input = File.ReadAllText(inputFilePath);
        }

        public int GetSumOfSizesOfDirectories(int threshold)
        {
            var dict = new Dictionary<string, int>();
            GetDirectoriesAtOrBelowThreshold(_rootDirectory, threshold, dict);
            return dict.Values.Sum();
        }

        public int GetSizeOfSmallestDirectoryAboveThreshold(int totalSpace, int requiredSpace)
        {
            var dict = new Dictionary<string, int>();

            var usedSpace = _rootDirectory.GetTotalRecursiveSize();
            var availableSpace = totalSpace - usedSpace;
            var spaceToDelete = requiredSpace - availableSpace;

            GetDirectoriesAtOrAboveThreshold(_rootDirectory, spaceToDelete, dict);
            return dict.OrderBy(d => d.Value).First().Value;
        }

        private void GetDirectoriesAtOrBelowThreshold(Directory currentDirectory, int threshold, Dictionary<string, int> dict)
        {
            var size = currentDirectory.GetTotalRecursiveSize();

            if (size <= threshold)
            {
                dict.Add(currentDirectory.FullyQualifiedName, size);
            }

            foreach (var child in currentDirectory.Children)
            {
                GetDirectoriesAtOrBelowThreshold(child, threshold, dict);
            }
        }

        private void GetDirectoriesAtOrAboveThreshold(Directory currentDirectory, int threshold, Dictionary<string, int> dict)
        {
            var size = currentDirectory.GetTotalRecursiveSize();

            if (size >= threshold)
            {
                dict.Add(currentDirectory.FullyQualifiedName, size);
            }

            foreach (var child in currentDirectory.Children)
            {
                GetDirectoriesAtOrAboveThreshold(child, threshold, dict);
            }
        }

        public void ParseInput()
        {
            var readingListing = false;
            var listing = new List<string>();

            var lines = _input.Split(Environment.NewLine);
            foreach (var line in lines)
            {
                // end of listing
                if (IsCommand(line) && readingListing) 
                {
                    ProcessListing(listing);
                    listing.Clear();
                    readingListing = false;
                }

                if (readingListing)
                {
                    listing.Add(line);
                }

                if (IsChangeToRoot(line))
                {
                    _rootDirectory = _currentDirectory = new Directory("/", null);
                    _parentDirectory = null;
                    continue;
                }

                if (IsDirectoryListing(line))
                {
                    readingListing = true;
                }

                if (IsChangeDirectory(line, out var directoryName))
                {
                    if (directoryName.Equals(".."))
                    {
                        _currentDirectory = _parentDirectory;
                        _parentDirectory = _currentDirectory.Parent;
                    }
                    else
                    {
                        _parentDirectory = _currentDirectory;
                        _currentDirectory = _parentDirectory.GetChildByName(directoryName);
                    }
                }
            }

            // clean up any pending listing at end of file
            if (readingListing)
            {
                    ProcessListing(listing);
                    listing.Clear();
                    readingListing = false;
            }
        }

        private void ProcessListing(List<string> listing)
        {
            foreach (var entry in listing)
            {
                // is a directory
                if (entry.StartsWith("dir"))
                {
                    var dirName = entry.Substring(4);
                    if (!_currentDirectory.Children.Any(c => c.Name == dirName))
                    {
                        _currentDirectory.Children.Add(new Directory(dirName, _currentDirectory));
                    }
                }
                else // is a file
                {
                    var values = entry.Split(" ");
                    if (!_currentDirectory.Files.ContainsKey(values[1]))
                    {
                        _currentDirectory.Files.Add(values[1], int.Parse(values[0]));
                    }
                }
            }
        }

        private bool IsCommand(string line) => line.StartsWith("$");

        private bool IsChangeDirectory(string line, out string directoryName)
        {
            directoryName = string.Empty;
            if (!line.StartsWith("$ cd")) return false;

            directoryName = line.Substring(5);
            return true;
        }

        private bool IsChangeToRoot(string line) => line.Equals("$ cd /");

        private bool IsDirectoryListing(string line) => line.Equals("$ ls");
    }

    public class Directory
    {
        public Directory Parent { get; }
        public Dictionary<string, int> Files { get; } = new Dictionary<string, int>();
        public List<Directory> Children { get; } = new List<Directory>();

        public string Name { get; }

        public string FullyQualifiedName
        {
            get 
            {
                var stack = new Stack<string>();
                
                var parent = this;
                while (parent != null)
                {
                    stack.Push(parent.Name);
                    parent = parent.Parent;
                }

                var sb = new StringBuilder();
                while (stack.TryPop(out var name))
                {
                    sb.Append(name);
                    sb.Append("/");
                }

                return sb.ToString();
            }
        }

        public Directory(string name, Directory parent)
        {
            if (name != "/" && parent == null) throw new ArgumentException("Parent cannot be null if not root");
            Name = name;
            Parent = parent;
        }

        public Directory GetChildByName(string name) => Children.Single(c => c.Name.Equals(name));

        public int GetTotalRecursiveSize()
        {
            var size = Files.Values.Sum();

            foreach (var child in Children)
            {
                size += child.GetTotalRecursiveSize();
            }

            return size;
        }

        public void PrintTree(int indent = 0)
        {
            var tabs = new char[indent];
            for (var i = 0; i < indent; i++)
            {
                tabs[i] = ' ';
            }
            var tabStr = new String(tabs);

            Console.WriteLine($"{tabStr} - {Name} ({GetTotalRecursiveSize()})");
            indent++;

            tabs = new char[indent];
            for (var i = 0; i < indent; i++)
            {
                tabs[i] = ' ';
            }
            tabStr = new String(tabs);

            foreach (var file in Files)
            {
                Console.WriteLine($"{tabStr} - {file.Key} {file.Value}");
            }    

            foreach (var child in Children)
            {
                child.PrintTree(indent);
            }

            indent--;
        }

    }
}