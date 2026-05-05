using System.Collections.Generic;
namespace ContactBook;

public class ContactComparer : IComparer<Contact>
{
    public enum SortType
    {
        Fname,
        Lname,
        Phone,
        Email
    };

    private SortType sortType;
    public ContactComparer(SortType sortType)
    {
        SetSortType(sortType);
    }

    public SortType GetSortType()
    {
        return sortType;
    }

    public void SetSortType(SortType sortType)
    {
        this.sortType = sortType;
    }
    public int Compare(Contact? x, Contact? y)
    {
        if (x == null && y == null) return 0;
        if (x == null) return -1;
        if (y == null) return 1;

        int r = 0;

        switch (sortType)
        {
            case SortType.Lname:
                return
                    (r = string.Compare(x.GetLname(), y.GetLname(), StringComparison.OrdinalIgnoreCase)) != 0 ? r :
                    (r = string.Compare(x.GetFname(), y.GetFname(), StringComparison.OrdinalIgnoreCase)) != 0 ? r :
                    (r = string.Compare(x.GetPhone(), y.GetPhone(), StringComparison.OrdinalIgnoreCase)) != 0 ? r :
                    string.Compare(x.GetEmail(), y.GetEmail(), StringComparison.OrdinalIgnoreCase);

            case SortType.Phone:
                return
                    (r = string.Compare(x.GetPhone(), y.GetPhone(), StringComparison.OrdinalIgnoreCase)) != 0 ? r :
                    (r = string.Compare(x.GetFname(), y.GetFname(), StringComparison.OrdinalIgnoreCase)) != 0 ? r :
                    (r = string.Compare(x.GetLname(), y.GetLname(), StringComparison.OrdinalIgnoreCase)) != 0 ? r :
                    string.Compare(x.GetEmail(), y.GetEmail(), StringComparison.OrdinalIgnoreCase);

            case SortType.Email:
                return
                    (r = string.Compare(x.GetEmail(), y.GetEmail(), StringComparison.OrdinalIgnoreCase)) != 0 ? r :
                    (r = string.Compare(x.GetFname(), y.GetFname(), StringComparison.OrdinalIgnoreCase)) != 0 ? r :
                    (r = string.Compare(x.GetLname(), y.GetLname(), StringComparison.OrdinalIgnoreCase)) != 0 ? r :
                    string.Compare(x.GetPhone(), y.GetPhone(), StringComparison.OrdinalIgnoreCase);

            case SortType.Fname:
            default:
                return
                    (r = string.Compare(x.GetFname(), y.GetFname(), StringComparison.OrdinalIgnoreCase)) != 0 ? r :
                    (r = string.Compare(x.GetLname(), y.GetLname(), StringComparison.OrdinalIgnoreCase)) != 0 ? r :
                    (r = string.Compare(x.GetPhone(), y.GetPhone(), StringComparison.OrdinalIgnoreCase)) != 0 ? r :
                    string.Compare(x.GetEmail(), y.GetEmail(), StringComparison.OrdinalIgnoreCase);
        }
    }

        }
  