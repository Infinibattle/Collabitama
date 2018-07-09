# Collabitama

## Voting

Votes are made by using reactions on the top level comment of the Pull Request.

| Yes | No | Abstain |
|-----|----|---------|
| ![+1](https://assets-cdn.github.com/images/icons/emoji/unicode/1f44d.png "+1") | ![-1](https://assets-cdn.github.com/images/icons/emoji/unicode/1f44e.png "+1") | ![confused](https://assets-cdn.github.com/images/icons/emoji/unicode/1f615.png "confused") |

An `Abstain` vote counts when calculating quorum but is discarded when checking to see if the vote passed.

Voting for more than one option can be disabled on a per project basis.

## Synchronizing a fork
### 1. Clone your fork:
git clone git@github.com:YOUR-USERNAME/YOUR-FORKED-REPO.git

### 2. Add remote from original repository in your forked repository:
cd into/cloned/fork-repo
git remote add upstream git://github.com/ORIGINAL-DEV-USERNAME/REPO-YOU-FORKED-FROM.git
git fetch upstream

### 3. Rebase your fork from original repo to keep up with their changes:
git rebase upstream/master

### 4. Force push it to your fork.
git push --force
