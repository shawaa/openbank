# OpenWrks Developer Challenge

Build an HTTP API that allows users to connect and retrieve data from multiple banks

## What we want from you?
We don’t expect you to finish the challenge, in fact we don’t want you to. What we want to see is your approach to writing software and designing the system. We want to see testable, maintainable and clean code.

## Requirements
Add users via the api - You must add the bank and account number along with the user
You should prevent two users having the same account number
Retrieve all users via the api
Retrieve users accounts via the api (this must come from the connected bank)
Retrieve users account transactions via the api (this must come from the connected bank)
Account + Transactions must be returned in a unified format

## Banking data
We have provided a set of a banking APIs to connect to. These APIs provide mock account data to work with which this is representative of the type of data that we deal with every day. 

BizfiBank - http://bizfibank-bizfitech.azurewebsites.net/swagger/
FairWayBank - http://fairwaybank-bizfitech.azurewebsites.net/swagger/

Note: Account numbers must be 8 digits long and cannot start with 0

Over the next few iterations we will be adding the following features
Create a transaction enrichment pipeline - we want to categorise the transactions
Adding OWBank connection - We will now accept users that bank with OWBank
Weekly data refreshes - For all connected users we want to update their data every week

How would you ensure these features fit into the system you have started to build?

## Bonus Points
Uploaded to an online repository - Can we pull the code and run it
API is secured

## Help! 
Any questions should be sent to development@openwrks.com where one of our team might give you some tips and pointers. Good luck
