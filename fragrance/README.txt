- Layihənin backend-i C# DOTNET framework-u və MVC pattern ilə yazılıb.
- Frontend html, css, javascript və tailwind css ilə hazırlanıb. Tətbiqin dizaynı https://fiama-demo.myshopify.com/ saytınnan götürülüb.
- MacOS - da MSSQL serverinin yüklənməsində problem yaşandığınnan, DBMS-də postgresql istifadə olunub.
- Authentication və authorization üçün DOTNET-in hazır useAuthentication() və useAuthorization() middleware-ləri istifadə olunub.
Authentication və Authorization prosesleri cookies ilə baş verir. Müştərilər və admin-i fərqəndirmək üçün fərqli cookies-lər yaranır.
- Tətbiqi işə salmaq üçün database file-ni run edib table-ları yaradın, əks halda database-də table-lar yaranmasa tətbiqdə xəta baş verəcək.
Database fayl-ı Data folderində, db.sql adı ilə yerləşir. Tətbiqin işə salınmasında problem yaşanarsa, Video folder-ində tətbiqin necə
işədiyinin videorecordunu tapa bilərsiz.