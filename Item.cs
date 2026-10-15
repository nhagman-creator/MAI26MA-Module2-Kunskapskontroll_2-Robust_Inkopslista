// One item on the shopping list.
class Item //item object that will be stored in the ShoppingList (list). Item contain two hidden fields where the values from two parameters (I) Name and (II) Price are stored. These two values are initially passed on as arguments to the constructor 
{
    public string Name { get; set; } //auto property Name that wrap the hidden field that store the data
    public int Price { get; set; } //auto property Price that wwrap the hidden fieldthat store the data

    public Item(string name, int price) //Constructor that pass on 2 arguments through 2 parameters (name and price) when an Item object is created
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Name must contain characters.", nameof(name));
        }
        Name = name;

        if (price < 0)
        {
            throw new ArgumentOutOfRangeException(
                nameof(price),                              //Sends teh parametername (ex.paramName) "price"
                price,
                "Varans pris kan inte vara negativt."
            );
        }
        Price = price;
    }

    public override string ToString() //method that is called to print the values stored in two hidden fields in the Item object
    {
        return $"{Name} - {Price} kr";
    }
}
