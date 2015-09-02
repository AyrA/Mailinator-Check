# Mailinator-Check
Checks dynamically, if a given domain is a mailinator inbox

This application should continue to work, even if they change IP addresses.
There are no hardcoded IP addresses.
This application **does not** contact any web services.
It only makes requests to your DNS server.

## Requirements
This application is written for .NET 2.0. I did not use anything deprecated,
so you should be able to upgrade to a newer framework if you wish.

## How it works
The operation is quite simple:

1. Collect the IP address(es) of the main mailinator mailbox
2. Find the MX record of the supplied DNS name
3. Get the IP address(es) from the MX record
4. Compare the lists and check, if there is at least one matching entry

## How to use
The release binary can be used from the command line.
Simply run `Mailinator-Check.exe` and supply any number of DNS names as argument.
The return value is the number of mailinator domains found.

## Convert to DLL
To make this into a DLL to use in your existing projects,
simply set the project type to "Class Library" and delete Program.cs,
or exclude it from the project.

## Using in your application directly
To use this directly in your application, include the 3 files `cls*.cs` in your application.
Then use the namespace `Mailinator_Check`.

## DNS component
The included DNS component `clsDns.cs, clsRecordCollection.cs` can be used to do lookups directly by yourself if you wish.
The namespace is `WinAPI.DNS`.
This is a low level wrapper for DNS. You will have to work with pointers and possibly uninitialized objects.
Have a look at the Mailinator checked class itself on how to do lookups.
Since this is a wrapper for native functions, you may run into memory leaks if used improperly.
The RecordCollection class features a destructor, that cleans up,
but you still might want to free the references yourself,
if you plan on having your application running for a prolonged time.

## Possible additions (TODO)
Below is a list of possible additions to the project.
If you want one, either try to convince me,
that it is very important for you,
or fork this repository and make a pull request if you have implemented it yourself.

### IPv6 Support
The lookup functions are there, but I do not work yet with them.
I assume you would not have mailinator boxes on IPv6 and regular mailboxes on IPv4.
Many mails would go missing.

### Lookup cache
If the same domain is specified multiple times, it is also looked up each time.

### Output filtering
Give the user the ability to filter the output.
Instead of only supplying a return value,
allow him to print the list of all mailinator domains or all "clean" domains.
Either by additional command line arguments or by outputting the lists on `stdout` and `stderr`.

### Allow E-Mail addresses
Allow E-Mail addresses as arguments to require less scripting from the user side.

### Custom DNS server list
Allow the user to use a custom DNS server instead of the default server.

### Input from stdin
Allow the domains being put in from `stdin` instead of only by argument.
This allows a huge list to be filtered.

### Callback
Allow the user to specify an application,
that is called for each (non-)mailinator domain individually.

