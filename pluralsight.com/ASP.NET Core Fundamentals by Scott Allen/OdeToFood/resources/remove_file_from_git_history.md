# Remove file from GIT history

1. Remove local file: `git filter-branch --force --index-filter "git rm --cached --ignore-unmatch pluralsight.com/ASP.NET\ Core\ Fundamentals\ by\ Scott\ Allen/OdeToFood/OdeToFood/appsettings.json" --prune-empty --tag-name-filter cat -- --all`

2. Push to remote: `git push --force --all`
