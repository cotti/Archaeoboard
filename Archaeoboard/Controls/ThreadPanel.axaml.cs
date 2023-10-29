using Archaeoboard.Data.Models;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Archaeoboard.UI;

public partial class ThreadPanel : UserControl
{
    public static readonly StyledProperty<IEnumerable<PostDAO>> PostsProperty =
AvaloniaProperty.Register<ThreadPanel, IEnumerable<PostDAO>>(nameof(Posts), Array.Empty<PostDAO>(), false, Avalonia.Data.BindingMode.TwoWay, (x) => x is not null ? true : false);

    public IEnumerable<PostDAO> Posts
    {
        get { return GetValue(PostsProperty); }
        set
        { 
            SetValue(PostsProperty, value);
            ContentPanel.Text = ParsePostList();
        }
    }

    public static readonly StyledProperty<string> CssThemeProperty =
AvaloniaProperty.Register<ThreadPanel, string>(nameof(CssTheme), string.Empty, false, Avalonia.Data.BindingMode.TwoWay, (x) => x != "0" ? true : false);

    public string CssTheme
    {
        get { return GetValue(CssThemeProperty); }
        set { SetValue(CssThemeProperty, value); }
    }

    private static string ochTheme =
        """
                /* pseud0ch style for kareha by lolocaust and WAHa_06x36 */
        @font-face {
          font-family: 'submona';
          src: url('submona.woff') format('woff');
        }

        html {
        	padding: 0px;
        	margin: 0px;
        }

        body {
        	padding: 8px;
        	margin: 0px;
        }

        body.mainpage {
        	background: #C5AD99;
        	    background-image: url(data:image/gif;base64,R0lGODdhPAA8AJkAANCznMWtmbOektzApiwAAAAAPAA8AAAC/5SPqcvtCJ6c1IUAsgZ48wuG4qV1njYMZxmkbjYGQpipJeDmuo7zdYoTRSwiRGuFg3lio4HpswF9SJzhobpMSXsZAVQoS6KcpoH3MsPIZsAtVWAbDoVYziuZ3nhhnavWVASVFkYVNdbixKbH0mXQQ8I0FdERVnOi4tXWcqFVZXKAqAbJNEoTdUT28vSlZvUoFRmpxIdh87LXw1Z4cAc5wzFIulIbt+FS6MELVBJsMPK7utqTAmfrbAPoqAlCVknaOkWsclej7VzlRyYJAhc+slTllDNFbZBIq+10I3WGJks77cc4bCSgAcsUp1SESd9+hRv4Q9wiT/aWwWIHMEQaXP/GyNn52EEfjIrYoGGkpVHaGEuF2kTZiFASvm8qp+2YR4xlophqHEKpcwlFOWjO0IB6cGRQMBlMm86oAFXBnzVRHcwbs2xFDkQ8lt28aSzMPHWFCHHNKkaDo44etk5LM7DerFJ/6hSTM6xSWrUkgGDieOIIl46IgNJxl7JwrnY34tzViuQiw2c1+k662wmoVhqC3yD29YOPLnhIbqBE9hOl0tBmLht6VDqy5NPPOKmQYabFRko+I7OAtEQYoTZeTAaLPQslI3d8RHPWzXiQpc3DlnsD468Wy+g1p58S9WTL7yItG5E5DgPtxOa/v7u5sgSOP62ARWr+GFimUcEsurX/wraQcslVFxgVIdhSgg243GZfW7EBAo+BCwqFW3L+hSYGf0pYhAsT9ghESFo7CHRXNIq5ch86cCxw0FI0ZXdOEQlQtQCNMjqjwIvi6YgaSV/9qENIQA75VXCzxdBIgJ005NBF2rBz23MxKKVMN/14otYezXyYR4bAuccOfFFK80lZfWDAESAGAhQOAlvpFU1yKUmnJikuUjXNc5J0GSZT0iUE3gf9+IejRwO+duYay+3DzkWBIPSYkZHYc1h7NLGB0IEjPahHP0Zl9A4TrW2TJVmBYNTneJfSkGmKoi1ayjmqIjlZcaGwFxxtbjB1ymd2ihVprgaWEqqHUzZajSYFKYq3J7FgZneqn1zAgtxIomQEqilTFjcYcibimlodUjrKrXqyOWqZFAUAADs=);
        	color: #000000;
        }
        body.threadpage {
        	background: #EFEFEF;
        	color: #000000;
        }
        body.backlogpage {
        	background: #FFFFFF;
        	color: #000000;
        }

        a {
        	color: #0000FF;
        }
        a:visited {
        	color: #660099;
        }
        a:hover {
                color: #FF0000;
        }
        form {
        	margin: 0px;
        }
        ol ul, ul ol, ul ul, ol ol {
        	margin-top: 0;
        	margin-bottom: 0;
        }
        ol p:first-child, ul p:first-child {
        	margin-top: 0;
        }
        ol p:last-child, ul p:last-child {
        	margin-bottom: 0;
        }
        /* ssz */
        .aa {
        	font-family: Monapo, Mona, 'MS Pgothic', 'MS Pゴシック', IPAMonaPGothic, 'IPA モナー Pゴシック', submona !important;
        	overflow: auto;
        	word-break: keep-all;
        	white-space: nowrap;
        	font-size: 16px;
        }
        pre {
        	overflow: auto;
        }
        code {
        	font-family: monospace;
        }
        #posts .replytext > p, #posts .replytext > blockquote {
        	word-wrap: break-word;
        }
        /* EXTREMELY inelegant but it'll do for now... 
        @media screen and (max-width: 850px) {
        	textarea {
        		max-width: 500px;
        	}
        }*/
        textarea {
        	max-width: calc(100vw - 200px);
        }

        .outerbox {
        	background: #CCFFCC;
        	border: 1px outset grey;
        	padding: 7px;
        	margin-bottom: 1em;
        	margin-left: 2.5%;
        	margin-right: 2.5%;
        	clear: both;
        }
        .innerbox {
        	border: 1px inset grey;
        	padding: 6px;
        }
        #titlebox h1 {
        	font-size: 1.5em;
        	font-weight: normal;
        	padding: 0px;
        	margin: 0px;
        }
        #titlebox {
        	position: relative;
        }
        #titlebox .threadnavigation {
        	position: absolute;
        	right: 12px;
        	top: 12px;
        }
        #threadbox .threadlink {
        	padding-right: 0.5em;
        }
        #threadlinks {
        	text-align: right;
        	font-weight: bold;
        }
        #createbox h2 {
        	color: #000000;
        	font-size: 1.2em;
        	font-weight: bold;
        	margin: 0em 0em 0.5em 0.3em;
        }
        .threadcaptcha {
        	color: #000000;
        }



        body.threadpage #threads {
        	margin-left: 2.5%;
        	margin-right: 2.5%;
        	background: #CCFFCC;
        	color: #000000;
        	padding: 7px;
        	margin-bottom: 2em;
        	border: 1px outset grey;
        	clear: both;
        }

        body.mainpage #threads {
        	margin-left: 2.5%;
        	margin-right: 2.5%;
        	background: #CCFFCC;
        	color: #000000;
        	padding: 7px;
        	margin-bottom: 2em;
        	border: 1px outset grey;
        	clear: both;
        }

        body.backlogpage #threads {
        	background: #FFFFFF;
        }


        body.threadpage #threads h1 {
        	margin-left: 2.5%;
        	margin-right: 2.5%;
        	font-size: 2em;
        	font-weight: normal;
        	text-align: center;
        	padding: 0px;
        	margin: 0px;
        	border-top: 1px inset grey;
        	border-left: 1px inset grey;
        	border-right: 1px inset grey;
        }

        body.backlogpage #threads h1 {
        	display:none
        }

        body.threadpage #threadlist {
        	background: #CCFFCC;

        	font-family: serif;
        	margin: 0px 0px 7px 0px;
        	padding: 6px;
        	border-left: 1px inset grey;
        	border-right: 1px inset grey;
        	border-bottom: 1px inset grey;
        }

        body.backlogpage #threadlist {
        	background: #FFFFFF;
        	font-family: serif;
        	padding: 6px;
        }



        #oldthreadlist {
        	background: #FFFFFF;
        	font-family: serif;
        	margin: 1px;
        	padding: 3px;
        	margin-bottom: 7px;
        }
        #oldthreadlist .threadlink {
        	display: block;
        }
        #oldthreadlist .manage {
        	font-size: small;
        	margin-left: 1em;
        }

        #posts {
        	background: #EFEFEF;
        }
        .mainpage #posts .thread {
        	margin-left: 2.5%;
        	margin-right: 2.5%;
        	position: relative;
        	background: #EFEFEF;
        	border: 1px outset grey;
        	padding: 7px;
        	margin-bottom: 2em;
        	clear: both;
        }
        .mainpage #posts .sagethread {
        	margin-left: 2.5%;
        	margin-right: 2.5%;
        	position: relative;
        	background: #EFEFEF;
        	border: 2px inset grey;
        	padding: 9px;
        	margin-bottom: 2em;
        	clear: both;
        }
        .mainpage #posts h2 {
        	color: #CC1105;
        	font-size: 1.5em;
        	font-weight: bold;
        	margin: 0px 0px -1px 0px;
        	padding: 1em 0em 0em 0.3em;
        	border-top: 1px inset grey;
        	border-left: 1px inset grey;
        	border-right: 1px inset grey;
        }
        .mainpage #posts h2 a {
        	color: #CC1105;
        	text-decoration: none;
        }
        .mainpage #posts h2 a:hover {
        	text-decoration: underline;
        }
        .threadpage #posts h2 {
        	color: #CC1105;
        	font-size: 1.2em;
        	font-weight: normal;
        	border-top: 2px ridge white;
        	margin: 1em 0 0 0;
        	padding: 0em 0em 0.5em 0em;
        }
        #posts h2 small {
        	font-size: 1em;
        	font-weight: normal;
        }
        #posts .threadnavigation {
        	position: absolute;
        	right: 16px;
        	top: 10px;
        }
        #posts .replies {
        	clear: both;
        	margin: 0px;
        }
        .mainpage #posts .replies {
        	padding: 3px;
        	border-left: 1px inset grey;
        	border-right: 1px inset grey;
        }
        #posts .allreplies, #posts .firstreply, #posts .finalreplies {
        }



        #posts .reply {
        	clear: both;
        	margin: 0px 0px 0.5em 0px;
        }
        #posts .deletedreply {
        	clear: both;
        	margin: 0px 0px 1.2em 0px;
        }
        #posts h3 {
        	font-size: 1em;
        	font-weight: normal;
        	margin: 0px;
        	padding: 0px;
        }
        #posts .deletedreply h3 {
        	color: #AFAFAF;
        	font-weight: bold;
        }
        #posts h3 .replynum {
        	font-weight: bold;
        	margin-right: 0.3em;
        }
        .mainpage #posts h3 .replynum {
        	margin-left: 0.3em;
        }
        #posts h3 .replynum a {
        	color: #000000;
        	text-decoration: none;
        }
        #posts h3 .replynum a:hover {
                color:#DD0000;
        }
        #posts h3 .postername {
        	color: #117743;
        	font-weight: bold;
        }
        #posts h3 .postertrip {
        	color: #228854;
        }
        #posts .deletebutton {
        	/*font-size: small;
        	float: right;
        	position: relative;
        	/*top: -1.3em;*/
        	display: none;
        }
        #posts img.thumb, #posts a img {
        	border-width: 1px;
        	float: left;
        	margin: 0.5em 2em 0.5em 3em;
        }
        #posts .nothumbnail {
        	float: left;
        	display: inline; /* stupid IE bug */
        	border: 1px inset grey;
        	text-align: center;
        	padding: 1em 0.5em 1em 0.5em;
        	margin: 0.5em 2em 0.5em 2em;
        }
        #posts .replytext {
        	margin: 0.5em 0em 0em 3em;
        }
        #posts .replytext > :first-child {
        	margin-top: 0em;
        }
        #posts .replytext blockquote {
        	margin-left: 0em;
        	color: #789922;
        }
        #posts .replyabbrev {
        	color: #707070;
        	margin-left: 3em;
        	margin-bottom: 0.5em;
        }

        #posts form {
        	clear: both;
        	background: #EFEFEF;
        	color: #000000;
        	font-family: serif;
        	margin: 0px;
        }
        #posts form small a {
        	color: #000000;
        	text-decoration: none;
        }
        #posts form small a:hover {
        	color: #FF0000;
        	text-decoration: underline;
        }
        .threadpage #posts form {
        	padding-top: 0.5em;
        	border-top: 2px ridge white;
        }
        .mainpage #posts form {
        	padding: 3px;
        	border-left: 1px inset grey;
        	border-right: 1px inset grey;
        	border-bottom: 1px inset grey;
        }
        #posts input[type=submit] {
        }
        .postcaptcha {
        	color: #000000;
        }

        h4 {
        	color: #CC1105;
        	font-size: 1em;
        	font-weight: bold;
        	font-family: sans-serif;
        }


        #oldthreadlist {
        }
        #oldthreadlist th {
        	text-align: left;
        }
        #oldthreadlist .line0 {
        }
        #oldthreadlist .line1 {
        }
        #oldthreadlist a {
        	display: block;
        }


        #footer
        {
                text-align: center;
                font-size: 0.8em;
        }



        .errorpage h1, .errorpage h2 {
        	text-align: center;
        }

        /*.deletebutton {
        	opacity: 0.1;
        }
        .deletebutton:hover {
        	opacity: 0.5;
        }
        */
        """;
    private static string tinyTheme = """
        body {
            font-size: 13px;
            font-family: "DejaVu Sans", "Liberation Sans", Arial, Verdana, Tahoma, sans-serif ;
            padding: 0 1.5%;
            background: #C5AD99;
            background-image: url(data:image/gif;base64,R0lGODdhPAA8AJkAANCznMWtmbOektzApiwAAAAAPAA8AAAC/5SPqcvtCJ6c1IUAsgZ48wuG4qV1njYMZxmkbjYGQpipJeDmuo7zdYoTRSwiRGuFg3lio4HpswF9SJzhobpMSXsZAVQoS6KcpoH3MsPIZsAtVWAbDoVYziuZ3nhhnavWVASVFkYVNdbixKbH0mXQQ8I0FdERVnOi4tXWcqFVZXKAqAbJNEoTdUT28vSlZvUoFRmpxIdh87LXw1Z4cAc5wzFIulIbt+FS6MELVBJsMPK7utqTAmfrbAPoqAlCVknaOkWsclej7VzlRyYJAhc+slTllDNFbZBIq+10I3WGJks77cc4bCSgAcsUp1SESd9+hRv4Q9wiT/aWwWIHMEQaXP/GyNn52EEfjIrYoGGkpVHaGEuF2kTZiFASvm8qp+2YR4xlophqHEKpcwlFOWjO0IB6cGRQMBlMm86oAFXBnzVRHcwbs2xFDkQ8lt28aSzMPHWFCHHNKkaDo44etk5LM7DerFJ/6hSTM6xSWrUkgGDieOIIl46IgNJxl7JwrnY34tzViuQiw2c1+k662wmoVhqC3yD29YOPLnhIbqBE9hOl0tBmLht6VDqy5NPPOKmQYabFRko+I7OAtEQYoTZeTAaLPQslI3d8RHPWzXiQpc3DlnsD468Wy+g1p58S9WTL7yItG5E5DgPtxOa/v7u5sgSOP62ARWr+GFimUcEsurX/wraQcslVFxgVIdhSgg243GZfW7EBAo+BCwqFW3L+hSYGf0pYhAsT9ghESFo7CHRXNIq5ch86cCxw0FI0ZXdOEQlQtQCNMjqjwIvi6YgaSV/9qENIQA75VXCzxdBIgJ005NBF2rBz23MxKKVMN/14otYezXyYR4bAuccOfFFK80lZfWDAESAGAhQOAlvpFU1yKUmnJikuUjXNc5J0GSZT0iUE3gf9+IejRwO+duYay+3DzkWBIPSYkZHYc1h7NLGB0IEjPahHP0Zl9A4TrW2TJVmBYNTneJfSkGmKoi1ayjmqIjlZcaGwFxxtbjB1ymd2ihVprgaWEqqHUzZajSYFKYq3J7FgZneqn1zAgtxIomQEqilTFjcYcibimlodUjrKrXqyOWqZFAUAADs=);
            position: relative;
            min-height: 97%;
            color: black;
        }

        textarea {
            max-width: 89vw;
            min-width: 40vw;
            min-height: 20vh;
        }

        input, textarea{
            -webkit-box-sizing: border-box;
               -moz-box-sizing: border-box; 
                    box-sizing: border-box; 
                    width: 95%;
        }

        #topicbox input.submit {
            width: initial;
        }

        input:not(.submit), textarea {
            background-color: #f7f7f7;
            border: 1px solid #ababab;
        }

        td.label {
            text-align: right;
        }

        #page_content {
            padding-bottom: 70px;
        }

        body#thread, body#preview, body#notice {
            background: #EFEFEF;
            padding: 0;
        }

        table {
            font-size: 12px;
            margin-top: 5px;
        }

        .footer {
            position: absolute;
            left: 0;
            right: 0;
            bottom: 0;
            font-size: 0.8em;
        }

        a {
            color: #00C;
            display: inline-block;
        }

        a:hover {
            color: #F00;
        }

        th {
            background-color: #c7c7c7;
        }

        td.main {
            width: 90%;
        }

        #pages a, .links a {
            margin-right: 4px;
        }

        .noscreen {
            display: none !important;
            border: 1px solid black;
        }

        .menu {
            background: #CFC;
        }
        .outer{
            background-color: #EFEFEF;
        }

        #topicbox {
            background-color: #CFC;
            margin-bottom: 0;
        }

        .menu, .outer {
            margin-bottom:2em;
            border-width: 1px;
            border-style: outset;
            padding: 6px;
            border-color: gray;
        }

        .inner {
            border-width: 1px; 
            border-style: inset; 
            padding: 6px; 
            border-color: gray;
            display: block;
        }

        .board_name {
            margin: 0;
        }


        @supports (-moz-appearance:meterbar) and (display:flex) {
            .menu,.outer,.inner {
                border-style: solid;
                border-color: black;
            }
        }

        #threadlist .links {
            font-weight: bold;
            font-size: 14px;
            padding-bottom: 5px;
        }
        #threadlist .thread {
            margin-right: 0.3em;
        }
        .subject h1, .subject h2 {
            display: inline-block;
            margin: 0;
            line-height: 120%;
            font-size: 1.5em;
            padding-bottom: 8px;
            color: #F00;
        }
        .subject a {
            text-decoration: none;
            color: #F00;
        }
        .subject h2 a:hover {
            text-decoration: underline;
        }

        span > select {
            margin-top: 5px;
        }

        h3.posthead {
            font-size: 1em;
            font-weight: normal;
            margin: 0;
        }

        .post .num, .post button {
            font-weight: bold;
            cursor: pointer;
            border: none;
            background: none;
            padding: 0;
        }
        .post .name {
            font-weight: bold;
            color: green;
        }
        .post .trip {
            color: green;
        }

        .post {
            overflow: auto;
            background-color: #EFEFEF;
        }

        .body {
            margin: .5em 0 1em 2em;
            word-wrap: break-word;
            position: relative;
        }
        .postform {
            display: inline-block;
        }
        .sjis, .aa, .thread_menu {
            font-family: Mona, "MS PGothic", Osaka, Meiryo, sans-serif;
            line-height: 1.1;
            display: inline;
        }
        code, .code {
            font: 100% Andale Mono,Courier New,monospace;
            display: inline;
            line-height: 120%;
            white-space: nowrap;
        }
        .precode {
            font: 100% Andale Mono,Courier New,monospace;
            display: inline;
            line-height: 120%;
            white-space: pre-wrap;
        }
        .spoiler {
              display: inline;
              background: #000;
              color: #000;
              padding: 0;
        }
        .spoiler:hover {
              color: #FFF;
        }
        .ascii,li,ol {
            margin: 0;
        }
        .ascii {
            display: inline;
            font-family: "Courier New", Monospace;
        }
        span.quote, blockquote {
            border-left-color: #666;
            color: #666;
            display:block;
            border-width: medium medium medium 2px; 
            border-style: none none none solid; 
            margin: 3px 0px; 
            padding: 0px 0px 0px 10px; 
        }

        .thread_menu a {
            float: right;
            text-decoration: initial;
            font-size: 14px;
            margin-left: 2px;}

        .recent {
            margin: 0.5em 0px;
        }

        #top_menu #styles {
            float: right;
        }

        .post.deleted {
            color: #9c9c9b;
        }

        .new_reply:after {
            content: "!";
            color: #f00;
            font-weight: bold;
            font-style: italic;
        }

        td.postfieldleft {
            text-align: center;
            font-weight: bold;
        }


        @media only screen and (max-device-width: 480px) {
            #top_menu #styles {
                font-size: smaller;
                width: 42%;
                text-align: right;
            }
            textarea {
                width: 99%;
            }

            form input {
                width: 100% !important;
            }

            .hljs, .body {
                overflow: scroll;
            }
            form tr {
                text-align: center;
            }

            form td {
                display: block;
            }

            form .btns {
                display: inline-table;
                text-align: center;
                padding-top: 5px;
            }

            form .label {
                text-align: left!important;
                padding-top: 5px;
            }

            .postform .submit {
                min-width: 100px;
            }

            #topicbox .submit {
                margin-top: 5px;
            }

            body {
                padding: 0;
            }

            .postform {
                display: block;
                text-align: center;
                width: 95%;
                margin: auto;
            }

            #threadlist .thread {
                display: block;
                margin-top: 1px;
            }

            a {
                word-break: break-all;
            }
        }

        .diff_display {
            white-space: pre-wrap;
            border: 1px solid #808080;
            background-color: #e8e8e8;
            padding: 25px;
        }
        .edited {
            display: table;
            font-style: italic;
            color: #808080;
            cursor: help;
        }
        .diff_display del, .diff_display ins {
            text-decoration:none;
        }

        .diff_display ins {
            color: #0ba205;
        }
        .diff_display del {
            color: #a20505;
        }

        .diff_display ins ins {
            color: #14cc2f;
            background-color: #c8f5c8;
        }
        .diff_display del del {
            color: #cc1414;
            background-color: #f5c8c8;
        }

        .diff_display del del:before {
            content:"- ";
            position: absolute;
            left: 10px;
        }

        .diff_display ins ins:before {
            content:"+ ";
            position: absolute;
            left: 10px;
        }


        .options {
            margin-top: 6px;
        }
        .options input {
            width: initial;
        }
        .diceroll {
            border: #b79292 2px solid;
            padding: 12px;
            display: inline-block;
            color: #3f3f71;
            background: #f1e7e7;
        }
        .d_high {
            color: green;
        }
        .d_low {
            color: red;
        }
        .d_mod {
            font-style: italic;
            font-size: smaller;
            color: #9E9E9E;
        }

        table.threads a {
            color: black;
        }

        table.threads {
            width: 100%;
        }

        #all_topics a {
            margin-right: 3px;
        }

        .guestbook {
            background-color: #f9f9f9;
            padding: 5px;
            border: 1px dashed black;
            margin-bottom: 10px;
        }

        span.backlinks {
            display: block;
            font-size: smaller;
            padding-left: 15px;
            font-style: italic;
            color:#484848
        }

        .prevcite {
            color: #f00;
        }

        .pln{color:#000}@media screen{.str{color:#080}.kwd{color: #00f;}.com{color: #b90000;}.typ{color:#606}.lit{color: #1e6161;}.clo,.opn,.pun{color:#660}.tag{color:#008}.atn{color:#606}.atv{color:#080}.dec,.var{color:#606}.fun{color:red}}@media print,projection{.kwd,.tag,.typ{font-weight:700}.str{color:#060}.kwd{color:#006}.com{color:#600;font-style:italic}.typ{color:#404}.lit{color:#044}.clo,.opn,.pun{color:#440}.tag{color:#006}.atn{color:#404}.atv{color:#060}}pre.prettyprint{padding:2px;border:1px solid #888}ol.linenums{margin-top:0;margin-bottom:0}li.L0,li.L1,li.L2,li.L3,li.L5,li.L6,li.L7,li.L8{list-style-type:none}li.L1,li.L3,li.L5,li.L7,li.L9{background:#eee}
        """;

    private static string pageHeaderStart = """
        <html><head>
                <style type="text/css">
        """;

    private static string pageHeaderEnd =
        """
        </style>
        </head>
        <body>
        """;

    private const string futabaTheme =
        """
        @font-face {
          font-family: 'submona';
          src: url('submona.woff') format('woff');
        }

        html {aaaa
        	padding: 0px;
        	margin: 0px;
        	background: #FFFFEE;
        }
        body {
        	padding: 8px;
        	margin: 0px;
        	background: #FFFFEE;
        	color: #800000;
        }
        a {
        	color: #0000EE;
        }
        a:visited {
        	color: #0000DD;
        }
        a:hover {
                color:#DD0000;
        }
        form {
        	margin: 0px;
        }
        ol ul, ul ol, ul ul, ol ol {
        	margin-top: 0;
        	margin-bottom: 0;
        }
        ol p:first-child, ul p:first-child {
        	margin-top: 0;
        }
        ol p:last-child, ul p:last-child {
        	margin-bottom: 0;
        }
        input, textarea {
        	border: 1px solid #800000;
        }
        textarea {
        	-moz-border-radius: 4px;
        }
        .aa {
        	font-family: Monapo, Mona, 'MS Pgothic', 'MS Pゴシック', IPAMonaPGothic, 'IPA モナー Pゴシック', submona !important;
        	overflow: auto;
        	word-break: keep-all;
        	white-space: nowrap;
        	font-size: 16px;
        }
        pre {
        	overflow: auto;
        }
        code {
        	font-size: 14px;
        	font-family: monospace;
        }
        #posts .replytext > p, #posts .replytext > blockquote {
        	word-wrap: break-word;
        }
        textarea {
        	max-width: calc(100vw - 200px);
        }

        #navigation {
        	font-size: small;
        	margin-bottom: 1em;
        	float: left;
        	white-space: nowrap;
        	background: #F0E0D6;
        	padding: 3px 0px 3px 0px;
        	margin: 0 0 1em 1em;
        	border: 1px solid #800000;
        	-moz-border-radius: 4px;
        }
        #navigation strong {
        	background: #EEAA88;
        	color: #800000;
        	font-weight: normal;
        	padding: 3px;
        	-moz-border-radius: 3px 0px 0px 3px;
        }
        #navigation a {
        	padding: 3px;
        }



        .outerbox, .backlogpage #threads {
        	background: #EEAA88;
        	color: #800000;
        	padding: 4px;
        	clear: both;
        }
        #titlebox {
        	-moz-border-radius: 6px 6px 0 0;
        }
        h1 {
        	background: #EEAA88;
        	font-size: 2em;
        	font-weight: normal;
        	text-align: center;
        	padding: 0px;
        	margin: 0px;
        }
        #titlebox {
        	position: relative;
        }
        #titlebox .threadnavigation {
        	position: absolute;
        	right: 0.3em;
        	top: 0.9em;
        }
        #rules, #threadbox .innerbox {
        	background: #F0E0D6;
        	margin: 3px;
        	padding: 6px;
        	-moz-border-radius: 4px;
        }
        #stylebox {
        	padding: 1px 9px;
        	margin: -1px 0;
        }
        #threadbox {
        	margin-bottom: 2em;
        	-moz-border-radius: 0 0 6px 6px;
        }
        #threadbox .threadlink {
        	padding-right: 0.5em;
        }
        #threadlinks {
        	text-align: right;
        	font-weight: bold;
        }
        #createbox {
        	-moz-border-radius: 4px;
        }
        #createbox form {
        	background: #F0E0D6;
        	color: #800000;
        	font-weight: bold;
        	margin: 3px;
        	padding: 3px;
        	-moz-border-radius: 4px;
        }
        #createbox h2 {
        	font-size: 1.2em;
        	font-weight: bold;
        	margin: 0 0 0 2em;
        	padding: 0px;
        	text-decoration: none;
        }
        .threadcaptcha {
        	color: #800000;
        }


        #posts {
        }
        #posts .thread {
        	background: #F0E0D6;
        	padding: 4px;
        	margin-bottom: 2em;
        	-moz-border-radius: 6px;
        	clear: both;
        }
        #posts .sagethread {
        	background: #F0E0D6;
        	border: 3px solid #EEAA88;
        	padding: 4px;
        	margin-bottom: 2em;
        	-moz-border-radius: 6px;
        	clear: both;
        }
        #posts h2 {
        	float: left;
        	color: #CC1105;
        	font-size: 1.2em;
        	font-weight: bold;
        	margin: 0px 0px 0px 1em;
        	padding: 0px;
        }
        #posts h2 a {
        	color: #CC1105;
        	text-decoration: none;
        }
        #posts h2 a:hover {
        	text-decoration: underline;
        }
        #posts h2 small {
        	font-size: 1em;
        	font-weight: normal;
        }
        #posts .threadnavigation {
        	float: right;
        }
        #posts .replies {
        	clear: both;
        	margin: 3px;
        	padding: 3px;
        }
        #posts .allreplies, #posts .firstreply, #posts .finalreplies {
        }
        #posts .repliesomitted {
        	clear: both;
        	background: #FFFFEE;
        	height: 4px;
        	margin: -0.3em 1em 0.5em 1em;
        	-moz-border-radius: 4px;
        }



        #posts .reply {
        	clear: both;
        	margin: 0px 0px 0.5em 0px;
        }
        #posts .deletedreply {
        	clear: both;
        	margin: 0px 0px 1.2em 0px;
        }
        #posts h3 {
        	font-size: 1em;
        	font-weight: normal;
        	margin: 0px;
        	padding: 0px;
        }
        #posts .deletedreply h3 {
        	color: #AFAFAF;
        	font-weight: bold;
        }
        #posts h3 .replynum {
        	font-weight: bold;
        	margin-left: 0.3em;
        	margin-right: 0.3em;
        }
        #posts h3 .replynum a {
        	color: #800000;
        	text-decoration: none;
        }
        #posts h3 .replynum a:hover {
                color:#DD0000;
        }
        #posts h3 .postername {
        	color: #117743;
        	font-weight: bold;
        }
        #posts h3 .postertrip {
        	color: #228854;
        }
        #posts .deletebutton {
        	font-size: small;
        	position: absolute;
        	right: 1em;
        }
        #posts img.thumb, #posts a img {
        	border: 1px solid #800000;
        	float: left;
        	margin: 0.5em 2em 0.5em 3em;
        }
        #posts .nothumbnail {
        	float: left;
        	display: inline; /* stupid IE bug */
        	background: #F0D7C7;
        	border: 2px dashed #EEAA88;
        	text-align: center;
        	padding: 1em 0.5em 1em 0.5em;
        	margin: 0.5em 2em 0.5em 2em;
        }
        #posts .replytext {
        	margin: 0.5em 0em 0em 3em;
        }
        #posts .replytext > :first-child {
        	margin-top: 0em;
        }
        #posts .replytext blockquote {
        	margin-left: 0em;
        	color: #789922;
        }
        #posts .replyabbrev {
        	color: #707070;
        	margin-left: 3em;
        	margin-bottom: 0.5em;
        }



        #posts form {
        	clear: both;
        	background: #F0E0D6;
        	color: #800000;
        	font-family: serif;
        	margin: 3px;
        	padding: 3px;
        	-moz-border-radius: 4px;
        	font-weight: bold;
        }
        #posts form a {
        	font-weight: normal;
        }
        .postcaptcha {
        	color: #800000;
        }



        #footer
        {
                text-align: center;
                font-size: 0.8em;
        }



        #oldthreadlist {
        	width: 100%;
        	background: #F0E0D6;
        	padding: 6px;
        	-moz-border-radius: 4px;
        	margin: 0 auto;
        }
        #oldthreadlist th {
        	text-align: left;
        }
        #oldthreadlist .line0 {
        	background: #F0E0D6;
        }
        #oldthreadlist .line1 {
        	background: #E0D1C7;
        }
        #oldthreadlist a {
        	display: block;
        }



        .errorpage h1, .errorpage h2 {
        	text-align: center;
        }
        
        """;

    private const string content =
        """
        <div id="posts">


        <a name="1"></a>

        <div class="thread">
        <h2><a href="/general/kareha.pl/1674664997/l50" rel="nofollow">modern search engines are garbage
        <small>(68)</small></a></h2>

        <div class="replies">

        <div class="firstreply">



        	<div class="reply"> <h3> <span class="replynum"><a title="Quote post number in reply" href="javascript:insert('&gt;&gt;1',1674664997)">1</a></span> Name:  <span class="postername">Anonymous</span><span class="postertrip"></span> : 2023-01-25 16:43 ID:G9mNWQnN  <span class="deletebutton">  <span class="manage" style="display:none;">[<a href="javascript:delete_post(1674664997,1)">Del</a>]</span> </span> </h3>  <div class="replytext"><p>When was the last time you used a search engine to find anything worthwhile? Even on a conceptual level they are a bad way to navigate the web and virtually every result page these days is muddled by algorithims built around marketing and spam. Search engines remove the interactive and community based nature of the internet. Remember when the only way to find sites was through word of mouth or spending hours procrastinating clicking on link after link? Today the average search result brings up a bunch of shit op eds and news articles sponsered by a company or just recyled press releases and reddit posts and its all set to get worse AI bullshit. </p></div> </div>



        	</div><div class="repliesomitted"></div><div class="finalreplies">


        	<div class="reply"> <h3> <span class="replynum"><a title="Quote post number in reply" href="javascript:insert('&gt;&gt;59',1674664997)">59</a></span> Name:  <span class="postername">Anonymous</span><span class="postertrip"></span> : 2023-09-16 16:25 ID:g6vidm4Z  <span class="deletebutton">  <span class="manage" style="display:none;">[<a href="javascript:delete_post(1674664997,59)">Del</a>]</span> </span> </h3>  <div class="replytext"><p><a href="/general/kareha.pl/1674664997/58" rel="nofollow">&gt;&gt;58</a><br />If you ever use proprietary software&#44; you don&#39;t care about your privacy at all. Using proprietary software means you&#39;re perfectly happy to entrust the owner of the software to act on your best interest. protip: they won&#39;t actually act on your best interest.</p></div> </div>




        	<div class="reply"> <h3> <span class="replynum"><a title="Quote post number in reply" href="javascript:insert('&gt;&gt;60',1674664997)">60</a></span> Name:  <span class="postername">Anonymous</span><span class="postertrip"></span> : 2023-09-16 18:16 ID:DcEQIZoq  <span class="deletebutton">  <span class="manage" style="display:none;">[<a href="javascript:delete_post(1674664997,60)">Del</a>]</span> </span> </h3>  <div class="replytext"><p><a href="/general/kareha.pl/1674664997/59" rel="nofollow">&gt;&gt;59</a><br />I know. I use free software myself. What I don�t get is the whole �ordinary people should learn Emacs� thing. Everyone should be encouraged to use a comfy free OS and limit proprietary garbage as much as possible and know how to protect their privacy online from data raping corporations and the NSA. </p></div> </div>




        	<div class="reply"> <h3> <span class="replynum"><a title="Quote post number in reply" href="javascript:insert('&gt;&gt;61',1674664997)">61</a></span> Name:  <span class="postername">Anonymous</span><span class="postertrip"></span> : 2023-09-17 20:11 ID:vWnEKoVX  <span class="deletebutton">  <span class="manage" style="display:none;">[<a href="javascript:delete_post(1674664997,61)">Del</a>]</span> </span> </h3>  <div class="replytext"><p>yeah&#44; searching the web on my computer. CPU intensive&#44; watch out! need a keyboard to type in all these website addresses. I know more URLs than a botnet has cyber-zombies. yeah&#44; i do a little typing&#44; i do a little Page Downing&#44; but you wont catch me doing a little scrolling thats for sure. Oh sorry&#44; did i just snowcrash ur whole datastruct? my bad&#44; heh. </p></div> </div>




        	<div class="reply"> <h3> <span class="replynum"><a title="Quote post number in reply" href="javascript:insert('&gt;&gt;62',1674664997)">62</a></span> Name:  <span class="postername">Anonymous</span><span class="postertrip"></span> : 2023-09-24 00:29 ID:TsUwztXl  <span class="deletebutton">  <span class="manage" style="display:none;">[<a href="javascript:delete_post(1674664997,62)">Del</a>]</span> </span> </h3>  <div class="replytext"><blockquote>&gt;everyone should use Linux<br />&gt;correct wey to use Linux is no gui&#44; terminal for everything&#44; browse web&#44; mail and fap to loli porn with command line </blockquote><p>Never before have I seen people whose enthusiasm for  their cause is matched only by their extreme elitism and gatekeeping. What do you Loonix morons want? Do you want Linux to be popular or do you want it to be something only basement dwelling neckbeards use? You can�t have it both ways. </p></div> </div>




        	<div class="reply"> <h3> <span class="replynum"><a title="Quote post number in reply" href="javascript:insert('&gt;&gt;63',1674664997)">63</a></span> Name:  <span class="postername">Anonymous</span><span class="postertrip"></span> : 2023-09-24 07:37 ID:iD8FuHyB  <span class="deletebutton">  <span class="manage" style="display:none;">[<a href="javascript:delete_post(1674664997,63)">Del</a>]</span> </span> </h3>  <div class="replytext"><p>Just use kde or xfce. Avoid gnome&#44; it sucks.</p></div> </div>




        	<div class="reply"> <h3> <span class="replynum"><a title="Quote post number in reply" href="javascript:insert('&gt;&gt;64',1674664997)">64</a></span> Name:  <span class="postername">Anonymous</span><span class="postertrip"></span> : 2023-09-24 16:31 ID:XWhVeXUe  <span class="deletebutton">  <span class="manage" style="display:none;">[<a href="javascript:delete_post(1674664997,64)">Del</a>]</span> </span> </h3>  <div class="replytext"><p>Agreed. A computer desktop should not look like a smartphone. Gnome is vomit inducing. </p></div> </div>




        	<div class="reply"> <h3> <span class="replynum"><a title="Quote post number in reply" href="javascript:insert('&gt;&gt;65',1674664997)">65</a></span> Name: <span class="postername"><a href="mailto:sage" rel="nofollow">Anonymous</a></span><span class="postertrip"><a href="mailto:sage" rel="nofollow"></a></span>  : 2023-09-26 15:58 ID:Heaven  <span class="deletebutton">  <span class="manage" style="display:none;">[<a href="javascript:delete_post(1674664997,65)">Del</a>]</span> </span> </h3>  <div class="replytext"><p>You watched one Luke Smith video and think you know everything... Many such cases.</p></div> </div>




        	<div class="reply"> <h3> <span class="replynum"><a title="Quote post number in reply" href="javascript:insert('&gt;&gt;66',1674664997)">66</a></span> Name:  <span class="postername">Anonymous</span><span class="postertrip"></span> : 2023-09-27 02:49 ID:XWhVeXUe  <span class="deletebutton">  <span class="manage" style="display:none;">[<a href="javascript:delete_post(1674664997,66)">Del</a>]</span> </span> </h3>  <div class="replytext"><p>Why does anyone need a search engine? Corporate algorithms shove more content up people�s anuses than torturers at Guantanamo. I bet with ChatGPT integration many people will stop using search engines altogether and spend their lives asking questions to chatbots or consume slop they see on Reddit/Twitter/Facebook and turn into mindless vegetables. </p></div> </div>




        	<div class="reply"> <h3> <span class="replynum"><a title="Quote post number in reply" href="javascript:insert('&gt;&gt;67',1674664997)">67</a></span> Name:  <span class="postername">Anonymous</span><span class="postertrip"></span> : 2023-09-27 20:20 ID:2LKEndZ6  <span class="deletebutton">  <span class="manage" style="display:none;">[<a href="javascript:delete_post(1674664997,67)">Del</a>]</span> </span> </h3>  <div class="replytext"><p><a href="/general/kareha.pl/1674664997/62" rel="nofollow">&gt;&gt;62</a><br />Whom are you quoting?</p><p>Anyway&#44; I use GNOME&#44; Wayland&#44; IntelliJ&#44; Steam&#44; I&#39;m feeling comfy for many years&#44; and I don&#39;t give a fuck what Youtube clowns are thinking about that.</p></div> </div>




        	<div class="reply"> <h3> <span class="replynum"><a title="Quote post number in reply" href="javascript:insert('&gt;&gt;68',1674664997)">68</a></span> Name:  <span class="postername">Anonymous</span><span class="postertrip"></span> : 2023-10-23 19:42 ID:mDJ+Yhsf  <span class="deletebutton">  <span class="manage" style="display:none;">[<a href="javascript:delete_post(1674664997,68)">Del</a>]</span> </span> </h3>  <div class="replytext"><p>Modern web browsers are garbage too </p></div> </div>





        </div>
        </div>


        </div>
        </div>
        """;

    private static string endContent = """
        </body></html>
        """;

    private string ParsePostList()
    {
        StringBuilder sb = new();
        foreach(var post in Posts)
        {
            sb.Append($"<div class=\"reply\"><h3><span class=\"replynum\"><a title=\"Quote post number in reply\" href=\"javascript:insert('&gt;&gt;{post.PostNumber}',{post.ThreadID})\">{post.PostNumber}</a></span> Name:  <span class=\"postername\">{post.PosterName}</span><span class=\"postertrip\">{post.PosterTrip}</span> : {post.PostDate} ID:{post.PosterID}</h3><div class=\"replytext\">{post.PostContent}</div></div>");
        }
        return sb.ToString();
    }
    public ThreadPanel()
    {
        InitializeComponent();
    }
}