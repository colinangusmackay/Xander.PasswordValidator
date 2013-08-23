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

jQuery.validator.addMethod(
  "xander-needs-number",
  function (value, element, parameters) {
  },
  "The password requires a number."
);

jQuery.validator.addMethod(
  "xander-needs-letter",
  function(value, element, parameters) {
  },
  "The password requires a letter."
);

jQuery.validator.addMethod(
  "xander-needs-symbol",
  function(value, element, parameters) {
  },
  "The password requires a symbol."
);

jQuery.validator.addMethod(
  "xander-standard-word-lists",
  function(value, element, parameters) {
  },
  "The password cannot contain a disallowed word."
);

jQuery.validator.addMethod(
  "xander-custom-word-lists",
  function(value, element, parameters) {
  },
  "The password cannot contain a disallowed word."
);