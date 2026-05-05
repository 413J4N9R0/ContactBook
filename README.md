# ContactBook
ContactBook assignment on C# which portays various functions such as

* NextPage
* PreviousPage
* GoToPage
* Page Size
* Create Contact
* Review Contact
* Update Contact
* Delete Contact
* Find Contacts
* Order Contacts
* Deduplicate/Merge Contacts
* Exit

Behind all these functions, there are various data structures and algorithms that make them work in harmony 

Among these data structures, there is : 

* Lists<T> - These are used to store the contacts that are gonna be used, and all the results for some of the functions, like the find/order function for fast access and flexibility in storage.

* Hash Tables (Dictionary) which is used in the COntact Merger.cs file to store similar components from contacts such as seen emails/phone numbers which is what was used. This structure also has a fast lookup and is very efficient for its used purpose, which is to group similar components.
  
* Arrays - Also used  in the Contact Merger file, but instead of storing similarities, its used to store the parent of the nodes or the parent of the duplicate sets.

* Disjoint Set Union (DSU) - is the data structure that allows to grab a certain value of (x) and sets them in a group based on which one it belongs to.

Among the Algorithms, there is: 

* Union-Find - This is basically an algorithm that keeps track of all the connected components for the deduplication function. It finds the root between two sets and merges two of the sets.
  
* HashBased Lookups - used to find matches within the contacts.
