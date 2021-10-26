const express = require('express');
const axios = require('axios');

const app = express();

app.use(express.json());
app.use(express.urlencoded({ extended: false }));

function compare(a,b) {
    // console.log( new Date(a.updated_at));
    return new Date(a.created_at).getTime() - new Date(b.created_at).getTime();
}

app.get('/', async (req, res) => {

    const api = await axios.get('https://api.github.com/orgs/takenet/repos')
    const repositoriosC = api.data.filter((repo) => repo.language === 'C#');
    const orderByCreatedDate = repositoriosC.sort(compare).slice(0,5);
    // console.log(orderByCreatedDate)
    res.status(200).json(orderByCreatedDate)
});

app.listen(3000, () => console.log('ouvindo porta 3000!'));