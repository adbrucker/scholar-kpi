(*** hide ***)
// This block of code is omitted in the generated HTML documentation. Use 
// it to define helpers that you do not want to show in the documentation.
#I "../../bin/LogicalHacking.ScholarKpi"

(**
LogicalHacking.ScholarKpi
======================

Documentation

<div class="row">
  <div class="span1"></div>
  <div class="span6">
    <div class="well well-small" id="nuget">
      The LogicalHacking.ScholarKpi library can be <a href="https://nuget.org/packages/LogicalHacking.ScholarKpi">installed from NuGet</a>:
      <pre>PM> Install-Package LogicalHacking.ScholarKpi</pre>
    </div>
  </div>
  <div class="span1"></div>
</div>

Example
-------

This example demonstrates using a function defined in this sample library.

*)
#r "LogicalHacking.ScholarKpi.dll"
open LogicalHacking.ScholarKpi.Core.Metrics

printfn "i10-index = %i" <| i10Index []

(**
Some more info

Samples & documentation
-----------------------

The library comes with comprehensible documentation. 
It can include tutorials automatically generated from `*.fsx` files in [the content folder][content]. 
The API reference is automatically generated from Markdown comments in the library implementation.

 * [Tutorial](tutorial.html) contains a further explanation of this sample library.

 * [API Reference](reference/index.html) contains automatically generated documentation for all types, modules
   and functions in the library. This includes additional brief samples on using most of the
   functions.
 
*)
