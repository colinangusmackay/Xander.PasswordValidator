#region Copyright notice
/******************************************************************************
 * Copyright (C) 2013 Colin Angus Mackay
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to 
 * deal in the Software without restriction, including without limitation the 
 * rights to use, copy, modify, merge, publish, distribute, sublicense, and/or
 * sell copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in 
 * all copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
 * FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS
 * IN THE SOFTWARE.
 * 
 ******************************************************************************
 *
 * For more information visit: 
 * https://github.com/colinangusmackay/Xander.PasswordValidator
 * 
 *****************************************************************************/
#endregion

using System;
using System.IO;
using Xander.PasswordValidator.Exceptions;

namespace Xander.PasswordValidator.Helpers
{
  public static class CustomWordListRetriever
  {
    public static string Retrieve(string fileName)
    {
      if (fileName == null) throw new ArgumentNullException("fileName");
      if (!File.Exists(fileName))
        throw new FileNotFoundException("A file containing a custom list of prohibited passwords was not found.", fileName);

      try
      {
        return RetrieveImpl(fileName);
      }
      catch (Exception ex)
      {
        throw new CustomValidationFileException("Unable to load the file containing the custom list of prohibited passwords.", fileName, ex);
      }
    }

    private static string RetrieveImpl(string fileName)
    {
      using (var fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read))
      {
        using (var reader = new StreamReader(fileStream))
        {
          return reader.ReadToEnd().Replace("\r\n","\n");
        }
      }
    }
  }
}