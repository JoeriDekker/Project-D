# Project-D

## Development progress branching rules
## Branches
- main: The main branch, production. Code has to be stable and tested before merging to main. Only gets merged to whenever a new release is ready.
- dev: The development branch. All feature branches should be branched off from this branch. This branch is merged to main when a new release is ready.
- feature branches: Branches for developing new features. Should be branched off from dev. When the feature is ready, it should be merged back to dev.

## Commit messages
- Use present tense, imperative mood. E.g. "Add feature" instead of "Added feature".
- Keep it short and concise. If more information is needed, use the description field.
- If using the description field, use bullet points for each point.