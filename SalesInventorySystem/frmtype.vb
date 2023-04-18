Public Class frmtype
    Dim Listview1 As ListViewItem
    Dim Listview2 As ListViewItem
    Dim Listview3 As ListViewItem


    Private Sub Form6_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
        Listview1 = lvfoodandproducts.Items.Add("Mayonnaise")
        Listview1 = lvfoodandproducts.Items.Add("Sandwich Spreads")
        Listview1 = lvfoodandproducts.Items.Add("Margarine")
        Listview1 = lvfoodandproducts.Items.Add("Cooking Oil")
        Listview1 = lvfoodandproducts.Items.Add("Ice Cream")
        Listview1 = lvfoodandproducts.Items.Add("Ketchup")
        Listview1 = lvfoodandproducts.Items.Add("Tea")
        Listview1 = lvfoodandproducts.Items.Add("Breakfast cereal")
        Listview1 = lvfoodandproducts.Items.Add("Soups")
        Listview1 = lvfoodandproducts.Items.Add("Seasoning")
        Listview1 = lvfoodandproducts.Items.Add("Peanut Butter")
        Listview1 = lvfoodandproducts.Items.Add("Milk")
        Listview1 = lvfoodandproducts.Items.Add("Mustard")
        Listview1 = lvfoodandproducts.Items.Add("Fruit Juice")
        Listview1 = lvfoodandproducts.Items.Add("Jams")

        Listview2 = lvpersonalcare.Items.Add("Soap")
        Listview2 = lvpersonalcare.Items.Add("Shower Gels")
        Listview2 = lvpersonalcare.Items.Add("Shampoo ")
        Listview2 = lvpersonalcare.Items.Add("Toothpaste")
        Listview2 = lvpersonalcare.Items.Add("Conditioner")
        Listview2 = lvpersonalcare.Items.Add("Deodorant")
        Listview2 = lvpersonalcare.Items.Add("Lotion")
        Listview2 = lvpersonalcare.Items.Add("Cosmetics")
        Listview2 = lvpersonalcare.Items.Add("Hair Wax")

        Listview3 = lvhouseholdcleaner.Items.Add("Dishwashing Detergent")
        Listview3 = lvhouseholdcleaner.Items.Add("Bleach")
        Listview3 = lvhouseholdcleaner.Items.Add("Laundry Soap")
        Listview3 = lvhouseholdcleaner.Items.Add("Laundry Bottle")
        Listview3 = lvhouseholdcleaner.Items.Add("Dishwashing Liquid")
        Listview3 = lvhouseholdcleaner.Items.Add("Laundry Detergent")
        Listview3 = lvhouseholdcleaner.Items.Add("House Cleaner")
        Listview3 = lvhouseholdcleaner.Items.Add("Air Freshener")
    End Sub

    Private Sub lvfoodandproducts_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles lvfoodandproducts.SelectedIndexChanged
        frmProduct.lbltype.Text = lvfoodandproducts.FocusedItem.Text

    End Sub

    Private Sub lvpersonalcare_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles lvpersonalcare.SelectedIndexChanged
        frmProduct.lbltype.Text = lvpersonalcare.FocusedItem.Text
    End Sub

    Private Sub lvhouseholdcleaner_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles lvhouseholdcleaner.SelectedIndexChanged
        frmProduct.lbltype.Text = lvhouseholdcleaner.FocusedItem.Text
    End Sub

    Private Sub btnok_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnok.Click
        Me.Close()

    End Sub
End Class