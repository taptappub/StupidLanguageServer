--liquibase formatted sql

--comment: -------- User --------

--changeset tsykanov:init-indexes-User-ExternalId splitStatements=false runOnChange=true runInTransaction=false failOnError=true
CREATE INDEX CONCURRENTLY IF NOT EXISTS "IX_User_ExternalId" ON "User" ("ExternalId");
--rollback DROP INDEX IF EXISTS "IX_User_ExternalId";



--comment: -------- Group --------

--changeset tsykanov:init-indexes-Group-UserId-IsDeleted-ExternalId splitStatements=false runOnChange=true runInTransaction=false failOnError=true
CREATE INDEX CONCURRENTLY IF NOT EXISTS "IX_Group_UserId_IsDeleted_ExternalId" ON "Group" ("UserId", "IsDeleted", "ExternalId");
--rollback DROP INDEX IF EXISTS "IX_Group_UserId_IsDeleted_ExternalId";



--comment: -------- Sentence --------

--changeset tsykanov:init-indexes-Sentence-UserId-IsDeleted-ExternalId splitStatements=false runOnChange=true runInTransaction=false failOnError=true
CREATE INDEX CONCURRENTLY IF NOT EXISTS "IX_Sentence_UserId_IsDeleted_ExternalId" ON "Sentence" ("UserId", "IsDeleted", "ExternalId");
--rollback DROP INDEX IF EXISTS "IX_Sentence_UserId_IsDeleted_ExternalId";




--comment: -------- SentenceWord --------

--changeset tsykanov:init-index-SentenceWord-SentenceId splitStatements=false runOnChange=true runInTransaction=false failOnError=true
CREATE INDEX CONCURRENTLY IF NOT EXISTS "IX_SentenceWord_SentenceId" ON "SentenceWord" ("SentenceId");
--rollback DROP INDEX IF EXISTS "IX_SentenceWord_SentenceId";

--changeset tsykanov:init-index-SentenceWord-WordId splitStatements=false runOnChange=true runInTransaction=false failOnError=true
CREATE INDEX CONCURRENTLY IF NOT EXISTS "IX_SentenceWord_WordId" ON "SentenceWord" ("WordId");
--rollback DROP INDEX IF EXISTS "IX_SentenceWord_WordId";





--comment: -------- Word --------

--changeset tsykanov:init-index-Word-UserId-IsDeleted-ExternalId splitStatements=false runOnChange=true runInTransaction=false failOnError=true
CREATE INDEX CONCURRENTLY IF NOT EXISTS "IX_Word_UserId-IsDeleted-ExternalId" ON "Word" ("UserId", "IsDeleted", "ExternalId");
--rollback DROP INDEX IF EXISTS "IX_Word_UserId-IsDeleted-ExternalId";

--changeset tsykanov:init-index-Word-GroupId splitStatements=false runOnChange=true runInTransaction=false failOnError=true
CREATE INDEX CONCURRENTLY IF NOT EXISTS "IX_Word_GroupId" ON "Word" ("GroupId");
--rollback DROP INDEX IF EXISTS "IX_Word_GroupId";
