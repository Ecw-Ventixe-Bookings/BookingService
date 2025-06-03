

namespace Application.Factories;

public static class EmailContentProvider
{
    public static string BookingConfirmationHtml(string eventTitle, DateTime eventDateTime, string location, string eventUrl) =>
    $@"
    <!DOCTYPE html>
    <html lang=""sv"">
    <head>
        <meta charset=""UTF-8"">
        <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
        <title>Din Bokningsbekräftelse</title>
    </head>
    <body
        style=
            ""margin: 0;
            padding: 0;
            background-color: #f7f7f7; /* --grey-20 */
            font-family: Arial, Helvetica, sans-serif;"">

        <table
            width=""90%""
            border=""0""
            cellspacing=""0""
            cellpadding=""0""
            style=
                ""background-color: #f7f7f7; /* --grey-20 */;
                margin-inline: auto;"">

            <tr>
                <td
                    align=""center""
                    style=
                        ""padding: 20px 0;"">

                    <table
                        border=""0""
                        cellspacing=""0""
                        cell cellpadding=""0""
                        style=
                            ""background-color: #ffffff; /* --grey-10 */
                            border-collapse: collapse;
                            border: 1px solid #e5e5e1; /* --grey-40 */"">

                        <tr>
                            <td
                                align=""center""
                                style=""
                                    padding: 20px 0 20px 0;
                                    background-color: #bdbdf9; /* --secondary-110 */
                                    /* --- SVG Ventixe Logo*/"">

                                <svg
                                    width=""40""
                                    height=""40""
                                    viewBox=""0 0 24 24""
                                    fill=""none""
                                    xmlns=""http://www.w3.org/2000/svg"">

                                    <path
                                        fill-rule=""evenodd""
                                        clip-rule=""evenodd""
                                        d=""M4.20134 13.5302L9 24H4H1V23.4631C1 21.838 1.33009 20.2298 1.97026 18.7361L4.20134 13.5302ZM19.7987 13.5302L15 24H20H23L23 23.4631C23 21.838 22.6699 20.2298 22.0297 18.736L19.7987 13.5302Z""
                                        fill=""#37437D""/>

                                    <path
                                        fill-rule=""evenodd""
                                        clip-rule=""evenodd""
                                        d=""M9.37617 22.5444C9.88827 21.6233 10.8714 21 12 21C13.1287 21 14.1117 21.6233 14.6238 22.5444L22.0297 5.26394C22.6699 3.77022 23 2.16203 23 0.536908V0H15C15 1.65686 13.6569 3 12 3C10.3431 3 9 1.65685 9 0H1V0.536908C1 2.16203 1.33009 3.77022 1.97026 5.26394L9.37617 22.5444Z""
                                        fill=""#F26CF9""/>

                                </svg>
                            </td>
                        </tr>

                        <tr>
                            <td
                                style=
                                    ""padding: 30px 30px 10px 30px;
                                    text-align: center;"">

                                <h1
                                    style=
                                        ""margin: 0;
                                        color: #37437d; /* --secondary-100 */
                                        font-size: 24px; font-weight: bold;"">

                                    Din Bokning är Bekräftad!

                                </h1>
                            </td>
                        </tr>

                        <tr>
                            <td
                                style=
                                    ""padding: 0 30px 20px 30px;
                                    color: #343435; /* --grey-90 */
                                    font-size: 16px;
                                    line-height: 1.5;
                                    text-align: center;"">

                                Tack för din bokning! Här är dina bokningsuppgifter:

                            </td>
                        </tr>

                        <tr>
                            <td
                                style=
                                    ""padding: 0 30px 20px 30px;
                                    color: #343435; /* --grey-90 */
                                    font-size: 16px;
                                    line-height: 1.5;
                                    text-align: left;"">
                                <strong>{eventTitle}</strong><br>
                                <strong>Tid:</strong> {eventDateTime.ToString()}<br>
                                <strong>Plats:</strong> {location}
                            </td>
                        </tr>

                        <tr>
                            <td
                                align=""center""
                                style=""padding: 20px 30px;"">
                                <a href=""{eventUrl}""
                                    style=""
                                        display: inline-block;
                                        background-color: #cd2ad3; /* --primary-100-dark */
                                        color: #ffffff;
                                        padding: 12px 25px;
                                        border-radius: 8px;
                                        text-decoration: none;
                                        font-weight: bold;"">
                                    Mer information
                                </a>
                            </td>
                        </tr>

                        <tr>
                            <td
                                style=
                                    ""padding: 20px 30px 10px 30px;
                                    color: #343435; /* --grey-90 */
                                    font-size: 14px;
                                    line-height: 1.5;
                                    text-align: center;"">
                                Vänligen spara denna bekräftelse för framtida referens.
                            </td>
                        </tr>

                        <tr>
                            <td
                                style=
                                    ""padding: 0 30px 30px 30px;
                                    color: #636365; /* --grey-80 */
                                    font-size: 14px;
                                    line-height: 1.5;
                                    text-align: center;"">
                                Om du har några frågor, vänligen kontakta oss.
                            </td>
                        </tr>

                        <tr>
                            <td
                                style=
                                    ""padding: 20px 30px;
                                    background-color: #ededfd; /* --grey-30 */
                                    text-align: center;
                                    color: #6f6f91; /* --grey-60 */
                                    font-size: 12px;"">

                                &copy; Mc Thunder Ventixe. Alla rättigheter förbehållna.<br>

                            </td>
                        </tr>

                    </table>
                </td>
            </tr>
        </table>
    </body>
    </html>
    ";
    
    public static string BookingComfirmationText(string eventTitle, DateTime eventDateTime, string location, string eventUrl) =>
        $@"
        ------------------------------------
        Ventixe - Bokningsbekräftelse
        ------------------------------------
        
        {eventTitle}
        {eventDateTime.ToString()}
        {location}

        Länk till eventet.
        {eventUrl}

        ------------------------------------

        Med vänliga hälsningar,
        Teamet på Ventixe

        ------------------------------------
        © Mc Thunder Ventixe. Alla rättigheter förbehållna.
        ";
}
