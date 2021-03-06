using System;

namespace MeetingFinder.Tests.Seed
{
    public static class EmployeeFileData
    {
        public static string GetWithEmployeeNamesBeforeTheirMeetings()
        {
            return $"192667735736991251148855951122872467118;Neil Greenlee{Environment.NewLine}" +
                   "192667735736991251148855951122872467118;2/12/2015 9:00:00 AM;2/12/2015 10:30:00 AM;" +
                   "C7EAF27D95F93AC1B4A28588EF3A635C90E917296FF37801E25619674604CEA5C156AD8A2C58A3E13D53592" +
                   "80E340FF9CEF95FCFB5A2D2E57805BD1CA8A795116A7AAAC48B6AD3FF6B7787787CC08462E2D80BAB5EF85FD6AF" +
                   $"CE196C3916AD70C838BF3DE2AF4F2E6DFC08E4B09EB1C0408A0447AF1E2E57B84D325CE47F8ADA{Environment.NewLine}" +
                   "192667735736991251148855951122872467118;2/12/2015 11:00:00 AM;2/12/2015 11:30:00 AM;" +
                   "C7EAF27D95F93AC1B4A28588EF3A635C90E917296FF37801E25619674604CEA5C156AD8A2C58A3E13D53592" +
                   "80E340FF9CEF95FCFB5A2D2E57805BD1CA8A795116A7AAAC48B6AD3FF6B7787787CC08462E2D80BAB5EF85FD6AF" +
                   $"CE196C3916AD70C838BF3DE2AF4F2E6DFC08E4B09EB1C0408A0447AF1E2E57B84D325CE47F8ADB{Environment.NewLine}" +
                   $"92667735736991251148855951122872467118;Phil Greenlee{Environment.NewLine}" +
                   "92667735736991251148855951122872467118;2/12/2015 9:00:00 AM;2/12/2015 10:30:00 AM;" +
                   "C7EAF27D95F93AC1B4A28588EF3A635C90E917296FF37801E25619674604CEA5C156AD8A2C58A3E13D53592" +
                   "80E340FF9CEF95FCFB5A2D2E57805BD1CA8A795116A7AAAC48B6AD3FF6B7787787CC08462E2D80BAB5EF85FD6AF" +
                   $"CE196C3916AD70C838BF3DE2AF4F2E6DFC08E4B09EB1C0408A0447AF1E2E57B84D325CE47F8ADA{Environment.NewLine}";
        }
        
        public static string GetWithEmployeeNamesAfterTheirMeetings()
        {
            return "192667735736991251148855951122872467118;2/12/2015 9:00:00 AM;2/12/2015 10:30:00 AM;" +
                    "C7EAF27D95F93AC1B4A28588EF3A635C90E917296FF37801E25619674604CEA5C156AD8A2C58A3E13D53592" +
                    "80E340FF9CEF95FCFB5A2D2E57805BD1CA8A795116A7AAAC48B6AD3FF6B7787787CC08462E2D80BAB5EF85FD6AF" +
                    $"CE196C3916AD70C838BF3DE2AF4F2E6DFC08E4B09EB1C0408A0447AF1E2E57B84D325CE47F8ADA{Environment.NewLine}" +
                    "192667735736991251148855951122872467118;2/12/2015 11:00:00 AM;2/12/2015 11:30:00 AM;" +
                    "C7EAF27D95F93AC1B4A28588EF3A635C90E917296FF37801E25619674604CEA5C156AD8A2C58A3E13D53592" +
                    $"80E340FF9CEF95FCFB5A2D2E57805BD1CA8A795116A7AAAC48B6AD3FF6B7787787CC08462E2D80BAB5EF85FD6AF{Environment.NewLine}" +
                    "92667735736991251148855951122872467118;2/12/2015 9:00:00 AM;2/12/2015 10:30:00 AM;" +
                    "C7EAF27D95F93AC1B4A28588EF3A635C90E917296FF37801E25619674604CEA5C156AD8A2C58A3E13D53592" +
                    "80E340FF9CEF95FCFB5A2D2E57805BD1CA8A795116A7AAAC48B6AD3FF6B7787787CC08462E2D80BAB5EF85FD6AF" +
                    $"CE196C3916AD70C838BF3DE2AF4F2E6DFC08E4B09EB1C0408A0447AF1E2E57B84D325CE47F8ADB{Environment.NewLine}" +
                    $"192667735736991251148855951122872467118;Neil Greenlee{Environment.NewLine}" +
                    $"92667735736991251148855951122872467118;Phil Greenlee{Environment.NewLine}";
        }
        
        public static string GetWithValidEmployessButWithNoMatchingMeetings()
        {
            return $"192667735736991251148855951122872467118;Neil Greenlee{Environment.NewLine}" +
                   "12667735736991251148855951122872467118;2/12/2015 9:00:00 AM;2/12/2015 10:30:00 AM;" +
                   "C7EAF27D95F93AC1B4A28588EF3A635C90E917296FF37801E25619674604CEA5C156AD8A2C58A3E13D53592" +
                   "80E340FF9CEF95FCFB5A2D2E57805BD1CA8A795116A7AAAC48B6AD3FF6B7787787CC08462E2D80BAB5EF85FD6AF" +
                   $"CE196C3916AD70C838BF3DE2AF4F2E6DFC08E4B09EB1C0408A0447AF1E2E57B84D325CE47F8ADA{Environment.NewLine}" +
                   "19267735736991251148855951122872467118;2/12/2015 11:00:00 AM;2/12/2015 11:30:00 AM;" +
                   "C7EAF27D95F93AC1B4A28588EF3A635C90E917296FF37801E25619674604CEA5C156AD8A2C58A3E13D53592" +
                   "80E340FF9CEF95FCFB5A2D2E57805BD1CA8A795116A7AAAC48B6AD3FF6B7787787CC08462E2D80BAB5EF85FD6AF" +
                   $"CE196C3916AD70C838BF3DE2AF4F2E6DFC08E4B09EB1C0408A0447AF1E2E57B84D325CE47F8ADA{Environment.NewLine}" +
                   $"92667735736991251148855951122872467118;Phil Greenlee{Environment.NewLine}" +
                   "9266775736991251148855951122872467118;2/12/2015 9:00:00 AM;2/12/2015 10:30:00 AM;" +
                   "C7EAF27D95F93AC1B4A28588EF3A635C90E917296FF37801E25619674604CEA5C156AD8A2C58A3E13D53592" +
                   "80E340FF9CEF95FCFB5A2D2E57805BD1CA8A795116A7AAAC48B6AD3FF6B7787787CC08462E2D80BAB5EF85FD6AF" +
                   $"CE196C3916AD70C838BF3DE2AF4F2E6DFC08E4B09EB1C0408A0447AF1E2E57B84D325CE47F8ADA{Environment.NewLine}";
        }
        
        public static string GetWithValidEmployessButInvalidMeetings()
        {
            return $"192667735736991251148855951122872467118;Neil Greenlee{Environment.NewLine}" +
                   "192667735736991251148855951122872467118;2/12/2015 900:00 AM;2/12/2015 10:30:00 AM;" +
                   "C7EAF27D95F93AC1B4A28588EF3A635C90E917296FF37801E25619674604CEA5C156AD8A2C58A3E13D53592" +
                   "80E340FF9CEF95FCFB5A2D2E57805BD1CA8A795116A7AAAC48B6AD3FF6B7787787CC08462E2D80BAB5EF85FD6AF" +
                   $"CE196C3916AD70C838BF3DE2AF4F2E6DFC08E4B09EB1C0408A0447AF1E2E57B84D325CE47F8ADA{Environment.NewLine}" +
                   "192667735736991251148855951122872467118;2/12/2015 11:00:00 A;2/12/2015 11:30:00 AM;" +
                   "C7EAF27D95F93AC1B4A28588EF3A635C90E917296FF37801E25619674604CEA5C156AD8A2C58A3E13D53592" +
                   "80E340FF9CEF95FCFB5A2D2E57805BD1CA8A795116A7AAAC48B6AD3FF6B7787787CC08462E2D80BAB5EF85FD6AF" +
                   $"CE196C3916AD70C838BF3DE2AF4F2E6DFC08E4B09EB1C0408A0447AF1E2E57B84D325CE47F8ADA{Environment.NewLine}" +
                   $"92667735736991251148855951122872467118;Phil Greenlee{Environment.NewLine}" +
                   "92667735736991251148855951122872467118;2/12/2015 9:00:00 AM;;" +
                   "C7EAF27D95F93AC1B4A28588EF3A635C90E917296FF37801E25619674604CEA5C156AD8A2C58A3E13D53592" +
                   "80E340FF9CEF95FCFB5A2D2E57805BD1CA8A795116A7AAAC48B6AD3FF6B7787787CC08462E2D80BAB5EF85FD6AF" +
                   $"CE196C3916AD70C838BF3DE2AF4F2E6DFC08E4B09EB1C0408A0447AF1E2E57B84D325CE47F8ADA{Environment.NewLine}";
        }
        
        public static string GetWithValidMeetingsButInvalidEmployees()
        {
            return "192667735736991251148855951122872467118;2/12/2015 9:00:00 AM;2/12/2015 10:30:00 AM;" +
                   "C7EAF27D95F93AC1B4A28588EF3A635C90E917296FF37801E25619674604CEA5C156AD8A2C58A3E13D53592" +
                   "80E340FF9CEF95FCFB5A2D2E57805BD1CA8A795116A7AAAC48B6AD3FF6B7787787CC08462E2D80BAB5EF85FD6AF" +
                   $"CE196C3916AD70C838BF3DE2AF4F2E6DFC08E4B09EB1C0408A0447AF1E2E57B84D325CE47F8ADA{Environment.NewLine}" +
                   "192667735736991251148855951122872467118;2/12/2015 11:00:00 AM;2/12/2015 11:30:00 AM;" +
                   "C7EAF27D95F93AC1B4A28588EF3A635C90E917296FF37801E25619674604CEA5C156AD8A2C58A3E13D53592" +
                   "80E340FF9CEF95FCFB5A2D2E57805BD1CA8A795116A7AAAC48B6AD3FF6B7787787CC08462E2D80BAB5EF85FD6AF" +
                   $"CE196C3916AD70C838BF3DE2AF4F2E6DFC08E4B09EB1C0408A0447AF1E2E57B84D325CE47F8ADA{Environment.NewLine}" +
                   "92667735736991251148855951122872467118;2/12/2015 9:00:00 AM;2/12/2015 10:30:00 AM;" +
                   "C7EAF27D95F93AC1B4A28588EF3A635C90E917296FF37801E25619674604CEA5C156AD8A2C58A3E13D53592" +
                   "80E340FF9CEF95FCFB5A2D2E57805BD1CA8A795116A7AAAC48B6AD3FF6B7787787CC08462E2D80BAB5EF85FD6AF" +
                   $"CE196C3916AD70C838BF3DE2AF4F2E6DFC08E4B09EB1C0408A0447AF1E2E57B84D325CE47F8ADA{Environment.NewLine}" +
                   $"192667735736991251148855951122872467118Neil Greenlee{Environment.NewLine}" +
                   $"Phil Greenlee{Environment.NewLine}";
        }
        
        public static string GetWithValidMeetingsButWithNoMatchingEmployees()
        {
            return "192667735736991251148855951122872467118;2/12/2015 9:00:00 AM;2/12/2015 10:30:00 AM;" +
                   "C7EAF27D95F93AC1B4A28588EF3A635C90E917296FF37801E25619674604CEA5C156AD8A2C58A3E13D53592" +
                   "80E340FF9CEF95FCFB5A2D2E57805BD1CA8A795116A7AAAC48B6AD3FF6B7787787CC08462E2D80BAB5EF85FD6AF" +
                   $"CE196C3916AD70C838BF3DE2AF4F2E6DFC08E4B09EB1C0408A0447AF1E2E57B84D325CE47F8ADA{Environment.NewLine}" +
                   "192667735736991251148855951122872467118;2/12/2015 11:00:00 AM;2/12/2015 11:30:00 AM;" +
                   "C7EAF27D95F93AC1B4A28588EF3A635C90E917296FF37801E25619674604CEA5C156AD8A2C58A3E13D53592" +
                   "80E340FF9CEF95FCFB5A2D2E57805BD1CA8A795116A7AAAC48B6AD3FF6B7787787CC08462E2D80BAB5EF85FD6AF" +
                   $"CE196C3916AD70C838BF3DE2AF4F2E6DFC08E4B09EB1C0408A0447AF1E2E57B84D325CE47F8ADA{Environment.NewLine}" +
                   "92667735736991251148855951122872467118;2/12/2015 9:00:00 AM;2/12/2015 10:30:00 AM;" +
                   "C7EAF27D95F93AC1B4A28588EF3A635C90E917296FF37801E25619674604CEA5C156AD8A2C58A3E13D53592" +
                   "80E340FF9CEF95FCFB5A2D2E57805BD1CA8A795116A7AAAC48B6AD3FF6B7787787CC08462E2D80BAB5EF85FD6AF" +
                   $"CE196C3916AD70C838BF3DE2AF4F2E6DFC08E4B09EB1C0408A0447AF1E2E57B84D325CE47F8ADA{Environment.NewLine}" +
                   $"1926677357369912511488559511228724671;Luigi Greenlee{Environment.NewLine}" +
                   $"9266773573699125114885595112287;Mario Greenlee{Environment.NewLine}";
        }
    }
}