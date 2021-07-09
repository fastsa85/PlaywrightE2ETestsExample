Feature: Product Details

Scenario: user should be able to add new product
	Given the user is on the Product Details page
	When the user enters product details
	| ProductName | ProductDescription               | ProductType   | ProductSupplier | ProductManufacturer |
	| Twix        | Caramel shortbread chocolate bar | Confectionery | Spar            | Mars Incorporated   |
	And the user clicks Submit button on the Product Details page
	Then list of products contains items as follows
	| ProductName | ProductDescription               | ProductType   | ProductSupplier | ProductManufacturer |
	| Twix        | Caramel shortbread chocolate bar | Confectionery | Spar            | Mars Incorporated   |