namespace ContactBook;

public class ContactMerger
{
    public static Dictionary<int, List<int>> FindDuplicates(List<Contact> contacts)
    {
        var phoneIndex = new Dictionary<string, int>();
        var emailIndex = new Dictionary<string, int>();

        var ds = new DuplicateSet(contacts.Count);

        for (int i = 0; i < contacts.Count; i++)
        {
            var c = contacts[i];

            string? phone = NormalizePhone(c.GetPhone());
            string? email = NormalizeEmail(c.GetEmail());

            
            if (!string.IsNullOrEmpty(phone))
            {
                foreach (var existing in phoneIndex)
                {
                    if (ArePhonesSimilar(existing.Key, phone))
                    {
                        ds.Union(existing.Value, i);
                    }
                }

                if (!phoneIndex.ContainsKey(phone))
                    phoneIndex[phone] = i;
            }

            
            if (!string.IsNullOrEmpty(email))
            {
                foreach (var existing in emailIndex)
                {
                    if (existing.Key == email)
                    {
                        ds.Union(existing.Value, i);
                    }
                }

                if (!emailIndex.ContainsKey(email))
                    emailIndex[email] = i;
            }
        }

        
        for (int i = 0; i < contacts.Count; i++)
            ds.FindRoot(i);

        var groups = new Dictionary<int, List<int>>();

        for (int i = 0; i < contacts.Count; i++)
        {
            int root = ds.FindRoot(i);

            if (!groups.ContainsKey(root))
                groups[root] = new List<int>();

            groups[root].Add(i);
        }

        return groups;
    }

  
    private static string? NormalizePhone(string phone)
    {
        if (string.IsNullOrWhiteSpace(phone))
            return null;

        var digits = new string(phone.Where(char.IsDigit).ToArray());

        return digits.Length > 0 ? digits : null;
    }

    private static bool ArePhonesSimilar(string a, string b)
    {
        if (a == b) return true;

        
        if (a.Length >= 7 && b.Length >= 7)
        {
            return a[^7..] == b[^7..];
        }

        return false;
    }

  
    private static string? NormalizeEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            return null;

        email = email.Trim().ToLowerInvariant();

        if (!email.Contains("@"))
            return null;

        var parts = email.Split('@');
        string local = parts[0];
        string domain = parts[1];

    
        if (domain == "gmail.com")
        {
            
            local = local.Replace(".", "");

            
            int plusIndex = local.IndexOf('+');
            if (plusIndex >= 0)
                local = local.Substring(0, plusIndex);
        }

        return $"{local}@{domain}";
    }

   
    private class DuplicateSet
    {
        private int[] parent;

        public DuplicateSet(int n)
        {
            parent = new int[n];
            for (int i = 0; i < n; i++)
                parent[i] = i;
        }

        public int FindRoot(int i)
        {
            if (parent[i] != i)
                parent[i] = FindRoot(parent[i]);

            return parent[i];
        }

        public void Union(int a, int b)
        {
            int rootA = FindRoot(a);
            int rootB = FindRoot(b);

            if (rootA != rootB)
                parent[rootB] = rootA;
        }
    }
}