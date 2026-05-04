using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Formats.Tar;

namespace ContactBook;

public class ContactBook
{

    public const string YES = "Y";
    public const string NO = "N";

    public readonly string[] YES_NO = new string[] { YES, NO };

    public const string NEXT_PAGE = "+";
    public const string PREV_PAGE = "-";
    public const string GOTO_PAGE = "G";
    public const string PAGE_SIZE = "S";
    public const string CREATE_CONTACT = "C";
    public const string REVIEW_CONTACT = "R";
    public const string UPDATE_CONTACT = "U";
    public const string DELETE_CONTACT = "D";
    public const string FIND_CONTACTS = "F";
    public const string ORDER_CONTACTS = "O";
    public const string DEDUPLICATE_CONTACTS = "M";
    public const string EXIT = "X";

    public readonly string[] COMMANDS = new string[]
    {
        NEXT_PAGE,
        PREV_PAGE,
        GOTO_PAGE,
        PAGE_SIZE,
        CREATE_CONTACT,
        REVIEW_CONTACT,
        UPDATE_CONTACT,
        DELETE_CONTACT,
        FIND_CONTACTS,
        ORDER_CONTACTS,
        DEDUPLICATE_CONTACTS,
        EXIT
    };


    private List<Contact> allcontacts;
    private int page;
    private int size;

    private bool isExit;

    public ContactBook(List<Contact>? contacts = null)
    {
        allcontacts = (contacts == null) ? new List<Contact>() : contacts;
        page = 1;
        size = 10;
        isExit = false;
    }


    public void Start()
    {
        ShowWelcomeScreen();

        string input;

        do
        {
            Console.Clear();
            ShowContacts();
            ShowInputOptions();

            input = GetInput();

            while (!IsValidInput(input))
            {
                input = GetInput();
            }

            ProcessInput(input);
        }
        while (!ConfirmExit());

        ShowExitScreen();
    }


    private void ShowWelcomeScreen()
    {
        Console.WriteLine("Welcome to Ale's Contact Book");
        PressEnterContinue();
    }

    private void ShowExitScreen()
    {
        Console.Clear();
        Console.WriteLine("Thank you for using le phone book, you're pretty cool.");
    }

    private void PressEnterContinue()
    {
        Console.WriteLine("Press ENTER to continue...");
        while (Console.ReadKey(true).Key != ConsoleKey.Enter) { }
    }

    private void ShowContacts()
    {
        ShowContacts(allcontacts, page, size);
    }

    private void ShowContacts(List<Contact> contacts, int page, int size)
    {
        Console.Clear();

        if (allcontacts.Count <= 0)
        {
            Console.WriteLine("No contacts found.");
            return;
        }

        int indexCol = Math.Max("#".Length, allcontacts.Count.ToString().Length);
        int fnameCol = Math.Max("First Name".Length, allcontacts.Max(c => c.GetFname()?.Length ?? 0));
        int lnameCol = Math.Max("Last Name".Length, allcontacts.Max(c => c.GetLname()?.Length ?? 0));
        int phoneCol = Math.Max("Phone".Length, allcontacts.Max(c => c.GetPhone()?.Length ?? 0));
        int emailCol = Math.Max("Email".Length, allcontacts.Max(c => c.GetEmail()?.Length ?? 0));

        Console.WriteLine(
            "{0," + -indexCol + "}  {1," + -fnameCol + "}  {2," + -lnameCol + "}  {3," + -phoneCol + "}  {4," + -emailCol + "}",
            "#", "First Name", "Last Name", "Phone", "Email"
        );

        Console.WriteLine(new string('-', indexCol + fnameCol + lnameCol + phoneCol + emailCol + 10));

        int n = allcontacts.Count;
        int pageCount = PageCount(size, n);

        int s = Math.Clamp((page - 1) * size, 0, n);
        int e = Math.Clamp(s + size, 0, n);

        for (int i = s; i < e; i++)
        {
            Contact c = allcontacts[i];

            Console.WriteLine(
                "{0," + indexCol + "}: {1," + fnameCol + "} {2," + lnameCol + "} {3," + phoneCol + "} {4," + emailCol + "}",
                i + 1, c.GetFname(), c.GetLname(), c.GetPhone(), c.GetEmail()
            );
        }
        for (int i = 0; i < size - e + s; i++)
        {
            Console.WriteLine();
        }
        Console.WriteLine();
        Console.WriteLine($"Page {page} of {pageCount} ({s + 1}-{e} of {n})");
    }


    private void ShowInputOptions()
    {
        string inputOptions =
            $"[{NEXT_PAGE}] Next Page | [{CREATE_CONTACT}] Create Contact | [{REVIEW_CONTACT}] Review Contact\n" +
            $"[{PREV_PAGE}] Previous Page | [{UPDATE_CONTACT}] Update Contact | [{DELETE_CONTACT}] Delete Contact\n" +
            $"[{GOTO_PAGE}] Go To Page | [{FIND_CONTACTS}] Find Contacts | [{ORDER_CONTACTS}] Order Contacts\n" +
            $"[{PAGE_SIZE}] Change Page Size | [{DEDUPLICATE_CONTACTS}] Deduplicate Contacts | [{EXIT}] Exit\n" +
            "\n> ";

        Console.WriteLine();
        Console.WriteLine(inputOptions);
    }

    private string GetInput()
    {
        return Console.ReadLine()!.ToUpper();
    }

    private bool IsValidInput(string input)
    {
        if (!COMMANDS.Contains(input))
        {
            Console.WriteLine("ERROR: Input Invalid, Please try again :)");
            PressEnterContinue();
            return false;
        }

        return true;
    }

   
    private void ProcessInput(string input)
    {
        switch (input)
        {
            case NEXT_PAGE: NextPage(); break;
            case PREV_PAGE: PreviousPage(); break;
            case GOTO_PAGE: GoToPage(); break;
            case PAGE_SIZE: PageSize(); break;
            case CREATE_CONTACT: CreateContact(); break;
            case REVIEW_CONTACT: ReviewContact(); break;
            case UPDATE_CONTACT: UpdateContact(); break;
            case DELETE_CONTACT: DeleteContact(); break;
            case FIND_CONTACTS: FindContacts(); break;
            case ORDER_CONTACTS: OrderContacts(); break;
            case DEDUPLICATE_CONTACTS: DeduplicateContacts(); break;
            case EXIT: Exit(); break;
        }
    }
    private void NextPage()
    {
        MoveToNextPage(allcontacts, ref page, size);
    }

    private void PreviousPage()
    {
        MoveToPreviousPage(allcontacts, ref page, size);
    }

    private void MoveToNextPage(List<Contact> contacts, ref int page, int size)
    {
        page = Math.Clamp(page + 1, 1, PageCount(size, contacts.Count));
    }

    private void MoveToPreviousPage(List<Contact> contacts, ref int page, int size)
    {
        page = Math.Clamp(page - 1, 1, PageCount(size, contacts.Count));
    }

    private static int PageCount(int size, int n)
    {
        return (int)Math.Max(1, Math.Ceiling(n / (double)size));
    }

    private void GoToPage()
    {
        Go2Page(allcontacts, ref page, size);
    }
    private void Go2Page(List<Contact> contacts, ref int page, int size )
    {
        page = GetInt("Enter page: ", 1, PageCount(size, contacts.Count));
    }
    private void PageSize()
    {
        PageSize(ref page, ref size);
    }

    private void PageSize(ref int page, ref int size)
    {
        int max = Console.WindowHeight - 10;
        size = GetInt("Enter page size", 1, max);
        page = 1;
    }
    private void CreateContact()
    {
        Console.Clear();
        Console.WriteLine(new string('#', 80));
        Console.WriteLine("Create Contact");
        Console.WriteLine(new string('#', 80));
        Console.WriteLine();
        Console.Write("Enter First Name:");
        string fname = Console.ReadLine()!;
        Console.Write("Enter Last Name:");
        string lname = Console.ReadLine()!;
        Console.Write("Enter phone number:");
        string phone = Console.ReadLine()!;
        Console.Write("Enter email address:");
        string email = Console.ReadLine()!;
        if (Confirm("Do you want to create this contact?", YES))
        {
            Contact c = new Contact(fname, lname, phone, email);
            allcontacts.Add(c);
            page = PageCount(size, allcontacts.Count);
            Console.WriteLine("Contact Added Succesfully");
        }
        else
        {
            Console.WriteLine("Operation Canceled. Contact was NOT saved.");
        }
        PressEnterContinue();
    }
    private void ReviewContact()
    {
        int index = GetInt("Enter Index", 1, allcontacts.Count) - 1;
        Console.Clear();
        OnReviewContact(index);
        PressEnterContinue();
    }
    private void OnReviewContact(int index)
    {
        Contact c = allcontacts[index];
        Console.WriteLine(new string('#', 80));
        Console.WriteLine("Review Contact");
        Console.WriteLine(new string('#', 80));
        Console.WriteLine();
        Console.WriteLine($"First Name:[{c.GetFname()}] ");
        Console.WriteLine($" Last Name:[{c.GetLname()}] ");
        Console.WriteLine($"     Phone:[{c.GetPhone()}] ");
        Console.WriteLine($"     Email:[{c.GetEmail()}] ");

        Console.WriteLine();
        


    }
    private void UpdateContact() { Console.WriteLine("Update Contact"); }
    private void DeleteContact() { Console.WriteLine("Delete Contact"); }
    private void FindContacts() { Console.WriteLine("Find Contacts"); }
    private void OrderContacts() { Console.WriteLine("Order Contacts"); }
    private void DeduplicateContacts() { Console.WriteLine("Deduplicate Contacts"); }
    private void Exit()
    {
        isExit = true;
 }

    private int GetInt(string prompt, int min, int max)
    {
         string options = $"{min}-{max}";

        Console.Write(prompt + $" [{options}]");
        string answer = Console.ReadLine()!;
        int value; 
        while (!int.TryParse(answer, out value) || value < min || value > max)
        {
            Console.WriteLine("ERROR: Input Invalid, Please try again :)");
           Console.Write(prompt + $" [{options}]");
            answer = Console.ReadLine()!;
        }

        return value;
    }
    private string GetOption(string prompt, string[] validOptions, string defaultOption)
    {
        string options = string.Join('/', validOptions);

        Console.Write(prompt + $" [{options}] ({defaultOption}) ");
        string option = Console.ReadLine()!.ToUpper();

        if (string.IsNullOrWhiteSpace(option))
            option = defaultOption;

        while (!validOptions.Contains(option))
        {
            Console.WriteLine("ERROR: Input Invalid, Please try again :)");
            Console.Write(prompt + $" [{options}] ({defaultOption}) ");
            option = Console.ReadLine()!.ToUpper();
        }

        return option;
    }

    private bool Confirm(string prompt, string defaultOption)
    {
        return GetOption(prompt, YES_NO, defaultOption) == YES;
    }

    private bool ConfirmExit()
    {
        return (isExit) ? isExit = Confirm("Do you want to exit?", NO) :  false;
    }
}