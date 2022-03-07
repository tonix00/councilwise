using System;
using System.Collections.Generic;

namespace CouncilWise
{
    /*	
   
    Requirements:

    - Given a list of receipt items where TaxAmount is not populated, calculate the correct tax for each receipt item. If IncludesGST is true, the unit price contains tax already. (In this case you will need to divide by 11 to get the tax amount). 
        If includesGST is false, the unit price does not contain tax and you will need to calculate it (multiply by 0.1 for tax amount). 
    - Return a populated Receipt object which contains the receipt items including tax amounts with the correct totals.
        Note that all totals shown on the receipt should be tax inclusive, including the receipt item totals.
    - For bonus points, if the receipt item name is a palindrome, the receipt item unit price should be changed to free. 
        A palindrome is a string which reads the same way forwards and backwards
    - You may modify the Receipt ReceiptItem and Helper classes any way you like as well as add new classes, but you must call the ProcessReceiptItems() method and the Receipt.ToString() method in your test case and no other methods.
    - 
    - Add additional test cases to cover any edge cases you can think of

    Expected Output:

    Print a receipt to the console with the following format:

    Receipt:
	    Items:
	    <Name>
		    <Quantity>		<UnitPrice>		<Total inc. Tax>
	    <Name>			
		    <Quantity>		<UnitPrice>		<Total inc. Tax>
	    ... for all receipt items
    Total:	<Total inc. Tax>
    GST:	<TaxTotal>

    */
    class Program
    {
        static void Main(string[] args)
        {
            RunTestCases();
        }

        /// <summary>
        /// Run through some possible scenarios for processing receipt items
        /// </summary>
        static void RunTestCases()
        {
            var items = new List<ReceiptItem>();
            items.Add(new ReceiptItem { Name = "Bouncy Ball", Quantity = 4, UnitPrice = 1.15m, IncludesTax = true });
            items.Add(new ReceiptItem { Name = "Doll's House", Quantity = 1, UnitPrice = 213.99m, IncludesTax = true });
            items.Add(new ReceiptItem { Name = "In-store assist hrs", Quantity = 2, UnitPrice = 25.30m, IncludesTax = false });
            var receiptResult = ProcessReceiptItems(items);
            Console.WriteLine(receiptResult.ToString());

            items = new List<ReceiptItem>();
            items.Add(new ReceiptItem { Name = "freebie eibeerf", Quantity = 4, UnitPrice = 1.15m, IncludesTax = true });
            receiptResult = ProcessReceiptItems(items);
            Console.WriteLine(receiptResult.ToString());

            // Added test. Mixed with palindrome string.. 
            items = new List<ReceiptItem>();
            items.Add(new ReceiptItem { Name = "Bouncy Ball", Quantity = 4, UnitPrice = 1.15m, IncludesTax = true });
            items.Add(new ReceiptItem { Name = "Doll's House", Quantity = 1, UnitPrice = 213.99m, IncludesTax = true });
            items.Add(new ReceiptItem { Name = "freebie eibeerf", Quantity = 4, UnitPrice = 1.15m, IncludesTax = true });
            items.Add(new ReceiptItem { Name = "In-store assist hrs", Quantity = 2, UnitPrice = 25.30m, IncludesTax = false });
            receiptResult = ProcessReceiptItems(items);
            Console.WriteLine(receiptResult.ToString());
        }

        /// <summary>
        /// Process a list of receipt items to ensure correct tax is allocated
        /// </summary>
        /// <param name="items"></param>
        /// <returns>processed receipt</returns>
        static Receipt ProcessReceiptItems(ICollection<ReceiptItem> items)
        {
            var total = 0.0m;
            var taxTotal = 0.0m;

            Console.WriteLine("Receipt:");
            Console.WriteLine("\tItems:");

            foreach (var item in items) 
            {
                var totalPrice = item.UnitPrice * item.Quantity;

                // If the receipt item name is a palindrome, the receipt item unit price should be changed to free.
                // Hence tax amount is also zero? Not sure, so just presumed it is.
                if (Helper.IsPalindrome(item.Name)) 
                {
                    totalPrice = 0.0m;
                    item.UnitPrice = 0.0m;
                    item.TaxAmount = 0.0m;
                }
                
                if (item.IncludesTax)
                    item.TaxAmount = totalPrice / 11; 
                else 
                    item.TaxAmount = totalPrice * Helper.TaxRate;
                
                total = totalPrice + total;
                taxTotal = item.TaxAmount + taxTotal;
                
                Console.WriteLine($"\t\tName:{item.Name}");
                Console.WriteLine($"\t\tQuantity:{item.Quantity} \t\t Unit Price:{item.UnitPrice} \t Tax Amount:{Helper.CurrencyRound(item.TaxAmount)}");
            }

            Console.WriteLine($"\tTotal:{total}");
            Console.WriteLine($"\tGST:{Helper.CurrencyRound(taxTotal)}");
            Console.WriteLine("\n");

            var receipt = new Receipt();
            receipt.Items = items;
            receipt.TaxTotal = taxTotal;
            receipt.Total = total;

            return receipt;
        }
    }
}
