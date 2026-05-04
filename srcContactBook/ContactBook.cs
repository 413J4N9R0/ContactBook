using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;

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

    public ContactBook(List<Contact>? contacts = null)
    {
        allcontacts = (contacts == null) ? new List<Contact>() : contacts;
    }

    public void Start()
    {
        ShowWelcomeScreen();

        string input;

        do
        {
            do
            {
                ShowContacts();
                ShowInputOptions();
                input = GetInput();
            }

            while (!IsValidInput(input));

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

    private void ShowContacts()
    {
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
            "{0," + -indexCol + "}  " +
            "{1," + -fnameCol + "}  " +
            "{2," + -lnameCol + "}  " +
            "{3," + -phoneCol + "}  " +
            "{4," + -emailCol + "}  ",
            "#", "First Name", "Last Name", "Phone", "Email"
        );

        Console.WriteLine(new string('-', indexCol + fnameCol + lnameCol + phoneCol + emailCol + 10));

        int page = 1;
        int size = 10;
        int n = allcontacts.Count;

        int pageCount = (int)Math.Max(1, Math.Ceiling(n / (double)size));
        int s = Math.Clamp((page - 1) * size, 0, n);
        int e = Math.Clamp(s + size, 0, n);

        for (int i = s; i < e; i++)
        {
            Contact c = allcontacts[i];

            Console.WriteLine(
                "{0," + indexCol + "}: " +
                "{1," + fnameCol + "} " +
                "{2," + lnameCol + "} " +
                "{3," + phoneCol + "} " +
                "{4," + emailCol + "}",
                i + 1, c.GetFname(), c.GetLname(), c.GetPhone(), c.GetEmail()
            );
        }

        Console.WriteLine();
        Console.WriteLine($"Page {page} of {pageCount} ({s + 1}-{e} of {n})");
    }

    private void ShowInputOptions()
    {
        string inputOptions = "" +
            $"[{NEXT_PAGE}] Next Page        | [{CREATE_CONTACT}] Create Contact | [{REVIEW_CONTACT}] Review Contact\n" +
            $"[{PREV_PAGE}] Previous Page    | [{UPDATE_CONTACT}] Update Contact | [{DELETE_CONTACT}] Delete Contact\n" +
            $"[{GOTO_PAGE}] Go To Page       | [{FIND_CONTACTS}] Find Contacts  | [{ORDER_CONTACTS}] Order Contacts\n" +
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
            Console.WriteLine("ERROR: Input Invalido, Please try again :)");
            PressEnterContinue();
            Console.Clear();
            return false;
        }
        else
        {
            return true;
        }


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
            default: break;
        }
    }

    private bool ConfirmExit()
    {
        return Confirm("Do you want to exit?", NO);
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

    private void NextPage()
    {
        Console.WriteLine("Next Page");

    }

    private void PreviousPage()
    {
        Console.WriteLine("Previous Page");
     
    }

    private void GoToPage()
    {
        Console.WriteLine("Go To Page");
    
    }

    private void PageSize()
    {
        Console.WriteLine("Change Page Size");
  
    }

    private void CreateContact()
    {
        Console.WriteLine("Create Contact");
      
    }

    private void ReviewContact()
    {
        Console.WriteLine("Review Contact");
       
    }

    private void UpdateContact()
    {
        Console.WriteLine("Update Contact");
      
    }

    private void DeleteContact()
    {
        Console.WriteLine("Delete Contact");
       
    }

    private void FindContacts()
    {
        Console.WriteLine("Find Contacts");
  
    }

    private void OrderContacts()
    {
        Console.WriteLine("Order Contacts");
    
    }

    private void DeduplicateContacts()
    {
        Console.WriteLine("Deduplicate Contacts");
    
    }

    private void Exit()
    {
        Console.WriteLine("Exit");
       
    }

    private string GetOptions(string prompt, string[] validOptions, string defaultOption)
    {
        string options = string.Join('/', validOptions); 
        Console.Write(prompt + $" [{options}] ({defaultOption}) ");
        string option = Console.ReadLine()!.ToUpper();

        if (string.IsNullOrWhiteSpace(option))
        {
            option = defaultOption;
        }

        while (!validOptions.Contains(option))
        {
            Console.WriteLine("ERROR: Input Invalido, Please try again :)");
            Console.Write(prompt + $" [{options}] ({defaultOption}) ");
            option = Console.ReadLine()!.ToUpper();
        }
        return option;
    }

    private bool Confirm(string prompt, string defaultOption)
    {
        return GetOptions(prompt, YES_NO, defaultOption) == YES;
    }

}