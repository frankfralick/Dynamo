![Image](https://raw.github.com/ikeough/Dynamo/master/doc/distrib/Images/dynamo_logo_dark.png) 
Dynamo is a Visual Programming language that aims to be accessible to both non-programmers and programmers alike. 
It gives users the ability to visually script behavior, define custom pieces of logic, and script using various textual programming languages.

The main Dynamo project includes an addin that runs on top of Autodesk Revit.  The purpose of this fork is to develop an implementation of Dynamo that runs within Autodesk Inventor.

This work is in no way endorsed by, or affiliated with Autodesk or any contributer to the original source.  This is a derivative work and contains modifications to the original source code.  
The main repository can be found [here](https://github.com/DynamoDS/Dynamo).

![Image](https://raw.github.com/frankfralick/Dynamo/Inventor_0.0.1/src/DynamoInventor/doc/images/DynamoInventor_2014_05_21_03.png)

## Why Dynamo and Inventor? ##
Dynamo will bring new possibilities and ways of working to Inventor.  Thoughtful integration of Dynamo into Inventor will provide powerful workflows that weren't possible before.  Dynamo intergration 
in Inventor will also create exciting new ways to collaborate with those using Dynamo in Revit.  Integration in Inventor will allow one Dynamo definition to drive representational building-scale designs 
in Revit, while simultaneously driving object-scale digital prototypes in Inventor.

##Roadmap##
There are several goals for this project.  The first effort has been around functionality that can take Dynamo generated geometric data, and use it as constraints for instantiated Inventor assemblies.  
There are many other desires for the project like broad API exposure, but mass-customization workflows are the main priority at the moment.  I will try to add a more detailed list of planned work.

Feedback on what you think would be useful to develop is greatly appreciated!  Submit them as issues!


## Releases ##

###Inventor_0.0.1###
* This isn't a release.  It is just the name of the branch you should use if you want to try this out.  There is no installer yet because I haven't finished writing all the malware I 
want to include.
* If you want to use this, you will need to build the project and install the .addin file/dlls manually.  If you really want this bad and don't know how to do that, please contact me (Frank) 
and I will be more than happy to help you get set up and get you the files you need.  


## Dynamo License ##

Those portions created by Ian are provided with the following copyright:

Copyright 2014 Ian Keough

Those portions created by Autodesk employees are provided with the following copyright:

Copyright 2014 Autodesk


Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.

## Instrumentation ##
Dynamo now contains an instrumentation system. This anonymously reports usage data to the Dynamo team that will be used to enhance the usability the product. Aggregated summaries of the data will be shared back with the Dynamo community.

An example of the data communicated is:

"DateTime: 2013-08-22 19:17:21, AppIdent: Dynamo, Tag: Heartbeat-Uptime-s, Data: MTMxMjQxLjY3MzAyMDg=, Priority: Info, SessionID: 3fd39f21-1c3f-4cf3-8cdd-f46ca5dde636, UserID: 2ac95f29-a912-49a8-8fb5-e2d287683d94"

The Data is Base64 encoded. For example, the data field above ('MTMxMjQxLjY3MzAyMDg=') decodes to: '131241.6730208' This represents the number of seconds that the instance of Dynamo has been running. 

The UserID is randomly generated when the application is first run. The SessionID is randomly generated each time Dynamo is opened.




## Third Party Licenses ##

###Avalon Edit###
http://www.codeproject.com/Articles/42490/Using-AvalonEdit-WPF-Text-Editor  
http://opensource.org/licenses/lgpl-3.0.html  

###Helix3D###
https://helixtoolkit.codeplex.com/  
https://helixtoolkit.codeplex.com/license  

###Iron Python###
http://ironpython.net/  
http://opensource.org/licenses/apache2.0.php  

###Kinect for Windows###
http://www.microsoft.com/en-us/kinectforwindows/  
http://www.microsoft.com/en-us/kinectforwindows/develop/sdk-eula.aspx 

###Microsoft 2012 C Runtime DLLS, msvcp110.dll and msvcr110.dll###
http://msdn.microsoft.com/en-us/vstudio/dn501987

###Moq###
http://www.nuget.org/packages/Moq/
http://opensource.org/licenses/bsd-license.php

###MiConvexHull###
http://miconvexhull.codeplex.com/  
http://miconvexhull.codeplex.com/license  

###NCalc###
http://ncalc.codeplex.com/  
http://ncalc.codeplex.com/license  

###NUnit####
http://www.nunit.org/  
http://www.nunit.org/index.php?p=license&r=2.6.2  

###OpenSans font from Google###
http://www.google.com/fonts/specimen/Open+Sans
http://www.apache.org/licenses/LICENSE-2.0.html

###Prism###
http://msdn.microsoft.com/en-us/library/gg406140.aspx  
http://msdn.microsoft.com/en-us/library/gg405489(PandP.40).aspx  


###ASM v. 219###
© 2014 Autodesk, Inc.  All rights reserved.   


All use of this Software is subject to the terms and conditions of the Autodesk license agreement accepted upon previous installation of Autodesk Revit or Autodesk Vasari. 



Trademarks  


Autodesk and T-Splines are registered trademarks or trademarks of Autodesk, Inc., and/or its subsidiaries and/or affiliates.  


Intel, Xeon and Pentium are registered trademarks or trademarks of Intel Corporation or its subsidiaries in the United States and other countries.  


Spatial, ACIS, and SAT are either registered trademarks or trademarks of Spatial Corp. in the United States and/or other countries. 


D-Cubed is a trademark of Siemens Industry Software Limited. 


Rhino is a trademark of Robert McNeel & Associates. 


All other brand names, product names or trademarks belong to their respective holders. 



Patents 

Protected by each of the following Patents: 7,274,364 



Third-Party Software Credits and Attributions 

This software is based in part on the works of the following: 


ACIS® © 1989–2001 Spatial Corp. 



Portions related to Intel® Threading Building Blocks v.4.1 are Copyright (C) 2005-2012 Intel Corporation.  All Rights Reserved. 



Portions related to Intel® Math Kernel Library v.10.0.3 (www.intel.com/software/products/mkl) are Copyright (C) 2000–2008, Intel Corporation. All rights reserved. 



This work contains the following software owned by Siemens Industry Software Limited: 


D-CubedTM HLM © 2013. Siemens Industry Software Limited. All Rights Reserved. 

D-CubedTM CDM © 2013. Siemens Industry Software Limited. All Rights Reserved.  



This Autodesk software contains CLAPACK v.3.2.1. 

Copyright (c) 1992-2011 The University of Tennessee and The University of Tennessee Research Foundation.  All rights reserved. 

Copyright (c) 2000-2011 The University of California Berkeley. All rights reserved. 

Copyright (c) 2006-2011 The University of Colorado Denver.  All rights reserved. 


Redistribution and use in source and binary forms, with or without modification, are permitted provided that the following conditions are met: 


- Redistributions of source code must retain the above copyright notice, this list of conditions and the following disclaimer. 


- Redistributions in binary form must reproduce the above copyright notice, this list of conditions and the following disclaimer listed in this license in the documentation and/or other materials provided with the distribution. 


- Neither the name of the copyright holders nor the names of its contributors may be used to endorse or promote products derived from this software without specific prior written permission. 


The copyright holders provide no reassurances that the source code provided does not infringe any patent, copyright, or any other intellectual property rights of third parties.  The copyright holders disclaim any liability to any recipient for claims brought against recipient by any third party for infringement of that parties intellectual property rights. 


THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE. 



This Autodesk software contains Eigen 3.2.0.  Eigen is licensed under the Mozilla Public License v.2.0, which can be found at http://www.mozilla.org/MPL/2.0/.   A text copy of this license and the source code for Eigen v.3.2.0 (and modifications made by Autodesk, if any) are included on the media provided by Autodesk or with the download of this Autodesk software.   
