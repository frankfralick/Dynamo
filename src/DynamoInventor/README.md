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

Dynamo's core developers are phasing out the previous FScheme evaluation engine and migrating existing services and node classes to use the DesignScript evaluation engine.  All the work on the Inventor branch of this repository targets FScheme, and moving forward the CheeseGrits branch of this repository will be the active development branch targeting DesignScript.

* ~~Create library project with nodes that work with DesignScript evaluation engine.~~
* Object binding, in and between sessions.  Dynamo's core developers are adding binding services to Dynamo that extensions will implement.  Once they are done this is top of the list.  
* ~~Write nodes for deep copying of assemblies that works with frame subassemblies.~~
* Write nodes for all work feature nodes.
* Develop work around for in-application use of Apprentice Server.  The plan is to do this via self hosting NancyFX. It's gonna be rad.
* Write extensions for conversion between DesignScript geometry objects and work features.
* ~~Write node for evaluating duplicate instance geometry among a list of geometries.  Purpose is for reuse of generated assemblies rather than a new assembly for each instance (like with iCopy).~~
* ~~Write part and assembly copy generating nodes.~~
* Start exposing more of Inventor's API through DSDynamoInventor library.
* Mountain of other things.


###Installation:
Clone this repository and checkout the CheeseGrits branch.  Before trying to
build you will need to setup an environmet variable called "INVENTORAPI"
that points to the version of Inventor's api you would like to target.

If you have Inventor and do not have Revit, you should be able to delete the
DynamoRevit project entirely and be able to build.  I have not tested this,
if you try please do let me  know.

