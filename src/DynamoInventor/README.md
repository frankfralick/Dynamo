#Dynamo and Inventor

The goal of this branch from the main Dynamo project is to add some useful
Dynamo extensions to Inventor.  Currently users interact with Dynamo in the
form of a Autodesk Revit addin, however, the authors of Dynamo have
structured it in a way where the core functionality of Dynamo does not
depend on Revit.  This makes it possible to code other application
API's to Dynamo with comparitively little effort.

While workflows that incorporate visual programming environments are
familiar to many types of CAD tools, I'm not aware of any examples of visual
programming methods being applied to history-based modellers.  More and more
design work, especially in the AEC space, is being done with visual
programming workflows.  Organizations that have to engineer and detail from
these types of designs could benefit from a similar toolset.

*Screenshot of Dynamo and Inventor (short demo can be seen here: http://youtu.be/A2A3QGtZgtM)*
![DynamoInventorExample01](https://raw.github.com/frankfralick/Dynamo/Inventor/src/DynamoInventor/Images/DynamoInventorCapture01.PNG)

###Short Term Roadmap:

* ~~Make reference key management stable.~~ For most object types, between
  session binding will work now.
* Write more work feature nodes.
* Write part placement node.
* Write assembly placement node.
* Write part and assembly copy generating nodes.
* Write mate constraint/mate constraint list node.
* Write flush constraint node.


###Installation:
Clone this repository and checkout the Inventor branch.  Before trying to
build you will need to setup an environmet variable called "INVENTORAPI"
that points to the version of Inventor's api you would like to target.

You will need FSharp 2.0 installed.

If you have Inventor and do not have Revit, you should be able to delete the
DynamoRevit project entirely and be able to build.  I have not tested this,
if you try please do let me  know.

