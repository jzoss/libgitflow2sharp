Extension methods for GitFlow using 100% libgit2sharp 


The concept is to add all the GitFlow commands to [Libgit2Sharp](https://github.com/libgit2/libgit2sharp).
There is no need to install the GitFlow extensions for this to work. 


Usage:

```c#

using (var repo = new Repository(pathtorepo))
{
     repo.Flow().StartFeature("featurename");
}

```
