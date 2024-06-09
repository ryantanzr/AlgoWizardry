
/**********************************************************
* Author: Ryan Tan
* This file contains abstractions for common behaviors that
* are used in the project
**********************************************************/

namespace Algowizardry.Utility
{
    public interface IDeepCopyable<T>
    {
        T DeepCopy();
    }

}