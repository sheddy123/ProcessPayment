# ProcessPayment
[![Build Status](https://dev.azure.com/FaultClearance/Sheddy%20Test/_apis/build/status/sheddy123.ProcessPayment?branchName=master)](https://dev.azure.com/FaultClearance/Sheddy%20Test/_build/latest?definitionId=4&branchName=master)
A WebAPI with only 1 method called “ProcessPayment” that receives a request and processes payment with the following fields like this-CreditCardNumber (mandatory, string, it should be a valid CCN)-CardHolder: (mandatory, string)-ExpirationDate (mandatory, DateTime, it cannot be in the past)-SecurityCode (optional, string, 3 digits)-Amount (mandatoy decimal, positive amount)
Also included in the project is a unit test for processing payment
