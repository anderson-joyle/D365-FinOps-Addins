![D365O Add-in](https://github.com/anderson-joyle/D365O-Addins/blob/master/D365O_addin_logo.png)

[![Donate Crypto](https://img.shields.io/badge/Donate-Crypto-805AFF.svg)](https://github.com/anderson-joyle/D365O-Addins#donate)
[![Say Thanks!](https://img.shields.io/badge/Say%20Thanks-!-1EAEDB.svg)](https://saythanks.io/to/joyle)

Dynamics 365 for Operation addins. Addins to give a hand at development time. 

Kindly note that there are much that can improved. Since that there is no official documentation for add-ins and tons of DLLs available, very likely there are clever ways to use the resources.

## Building the tool

The code should be easy to build in Visual Studio once you fix up the references to point to the place where your product specific assemblies live. The output from a successful compilation is an assembly that can subsequently be deployed using the batch file that is part of the project. Currently the command file is run when the compilation is completed. If this is not what you want, you can change the post-build event command line in the Build Events tab in the project properties.

Note: You will need to restart Visual Studio to get access to the new add-in.

## Using the tool

Once the tool is built and deployed as described above, it is available in the addins menu in the Dynamics 365 menu. This particular addin is designed to be applied on a selected designer node, so you have to open the table in the designer to use it; you cannot use it directly from the Application Explorer at this time.
Creating Add-ins of your own

It is easy to get started writing your own addins. Just open Visual Studio, create a new project and choose the template called "Developer tools Addin" from the "Dynamics 365 for Operations" set. This will create a project with examples of both a Mainmenu addin (i.e. an addin that is visible on the Addins menu on the Dynamics 365 menu, and that is not tied to any particular metadata artifact), and a Designer addin which is appears when a particular artifact is selected in the designer. There are TODO comments indicating where you should put your code.

You are more than welcome to contribute!

> PS. part of this text was copied from [SSMS Visual Studio add-in](https://github.com/Microsoft/ssms-visualstudio-addin)

## Donate
If this project helped you in any way and you feel like supporting me:

###### BTC: 1G3rHge15Kt1G8Amh2ZfNeH5pp1L3CFGmm
###### ETH: 0x9c8A747C13536b1de9Ce932E0f79FA4eB6E309b6
###### LTC: LMdh66L6tv19YLxbVgP1t47eocitSHhw5S
