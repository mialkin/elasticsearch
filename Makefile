.PHONY: run-infrastructure
run-infrastructure: copy-env
	docker-compose -f docker-compose.old.yml up

.PHONY: shutdown-infrastructure
shutdown-infrastructure:
	docker-compose -f docker-compose.old.yml down

.PHONY: copy-env
copy-env:
	cp -n .env.example .env | true
