"use strict";
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
jQuery(document).ready(function () {

  jQuery.validator.addMethod(
    "xanderNeedsNumber",
    function (value, element, parameters) {
      if (this.optional(element))
        return true;
      for (var i = 0; i < value.length; i++)
        if (value[i] >= '0' && value[i] <= '9')
          return true;
      return false;
    },
    "The password requires a number."
  );

  jQuery.validator.addMethod(
    "xanderNeedsLetter",
    function (value, element, parameters) {
      if (this.optional(element))
        return true;
      var lettersRegEx = /[a-zA-Z\u00AA\u00B5\u00BA\u00C0\u00C1\u00C2\u00C3\u00C4\u00C5\u00C6\u00C7\u00C8\u00C9\u00CA\u00CB\u00CC\u00CD\u00CE\u00CF\u00D0\u00D1\u00D2\u00D3\u00D4\u00D5\u00D6\u00D8\u00D9\u00DA\u00DB\u00DC\u00DD\u00DE\u00DF\u00E0\u00E1\u00E2\u00E3\u00E4\u00E5\u00E6\u00E7\u00E8\u00E9\u00EA\u00EB\u00EC\u00ED\u00EE\u00EF\u00F0\u00F1\u00F2\u00F3\u00F4\u00F5\u00F6\u00F8\u00F9\u00FA\u00FB\u00FC\u00FD\u00FE\u00FF]/;
      return lettersRegEx.test(value);
    },
    "The password requires a letter."
  );

  jQuery.validator.addMethod(
    "xander-needs-symbol",
    function (value, element, parameters) {
    },
    "The password requires a symbol."
  );

  jQuery.validator.addMethod(
    "xander-standard-word-lists",
    function (value, element, parameters) {
    },
    "The password cannot contain a disallowed word."
  );

  jQuery.validator.addMethod(
    "xander-custom-word-lists",
    function (value, element, parameters) {
    },
    "The password cannot contain a disallowed word."
  );
});
