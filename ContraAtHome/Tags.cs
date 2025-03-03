using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContraAtHome
{
    public abstract class Tags : PictureBox
    {
        private string[] tags { get; set; }
        public IReadOnlyList<string> ReadOnlyTags => Array.AsReadOnly(tags);
        public Tags(params string[] initialTags)
        {
            tags = new string[3];
            for (int i = 0; i < 3; i++)
            {
                tags[i] = i < initialTags.Length ? initialTags[i] : $"DefaultTag{i + 1}";
            }
        }
        public void DisplayTags()
        {
            Debug.WriteLine("Tags: " + string.Join(", ", ReadOnlyTags) + "\n");
        }
        public void ReplaceTag(int index, string newTag)
        {
            if (index < 0 || index >= tags.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(index), "Index must be within the range of the tags array.");
            }
            tags[index] = newTag;
        }

        public void GetTags()
        {
            foreach (var tag in tags)
            {
                Debug.WriteLine(tag);
            }
        }
        public string GetTags(int index) { 
            return (string)tags[index];
        }
    }
}
